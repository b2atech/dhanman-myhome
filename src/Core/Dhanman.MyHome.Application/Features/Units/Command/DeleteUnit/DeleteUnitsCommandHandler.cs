using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Units.Event;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Units.Command.DeleteUnit
{
    public class DeleteUnitCommandHandler : ICommandHandler<DeleteUnitCommand, Result<EntityDeletedResponse>>
    {
        #region Properties

        private readonly IUnitRepository _unitRepository;
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public DeleteUnitCommandHandler(IUnitRepository unitRepository, IMediator mediator)
        {
            _unitRepository = unitRepository;
            _mediator = mediator;
        }

        #endregion

        #region Methods
        public async Task<Result<EntityDeletedResponse>> Handle(DeleteUnitCommand request, CancellationToken cancellationToken)
        {
            var updateUnits = await _unitRepository.GetBydIdIntAsync(request.UnitId);

            if (updateUnits == null)
            {
                throw new InvoiceNotFoundException(request.UnitId);
            }

            updateUnits.IsDeleted = true;
            _unitRepository.Update(updateUnits);

            await _mediator.Publish(new UnitUpdateEvent(updateUnits.Id), cancellationToken);
            return Result.Success(new EntityDeletedResponse(updateUnits.Id));
        }

        #endregion
    }
}

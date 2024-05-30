using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Units.Event;
using Dhanman.MyHome.Domain.Abstractions;
using MediatR;
using Unit = Dhanman.MyHome.Domain.Entities.Units.Unit;

namespace Dhanman.MyHome.Application.Features.Units.Command.CreateUnits
{
    public class CreateUnitsCommandHandler : ICommandHandler<CreateUnitsCommand, Result<EntityCreatedResponse>>
    {
        #region Properties
        private readonly IUnitRepository _unitRepository;
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public CreateUnitsCommandHandler(IUnitRepository unitRepository, IMediator mediator)
        {
            _unitRepository = unitRepository;
            _mediator = mediator;
        }
        #endregion

        #region Methods
        public async Task<Result<EntityCreatedResponse>> Handle(CreateUnitsCommand request, CancellationToken cancellationToken)
        {
            int lastId = await _unitRepository.GetLastUnitIdAsync();
           
            int newId = lastId + 1;
                var unit = new Unit(newId, request.Name, request.BuildingId,
                   request.FloorId,
                   request.UnitTypeId,
                   request.OccupantId,
                   request.OccupancyId,
                   request.Area,
                   request.Bhk,
                   request.PhoneExtension,
                   request.EIntercom,
                    "1.0",
                    "1.1",
                    Guid.NewGuid()
                    );

            _unitRepository.Insert( unit );

            await _mediator.Publish(new UnitCreatedEvent(unit.Id), cancellationToken);

            return Result.Success(new EntityCreatedResponse(unit.Id));
        }
        #endregion
    }
}

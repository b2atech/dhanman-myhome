using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Units.Event;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Units.Command.UpdateUnit
{
    public class UpdateUnitCommandHandler: ICommandHandler<UpdateUnitCommand, Result<EntityUpdatedResponse>>
    {
        #region Properties
        private readonly IUnitRepository _unitRepository;
        private readonly IMediator _mediator;
        #endregion

        #region Constructor
        public UpdateUnitCommandHandler(IUnitRepository unitRepository, IMediator mediator)
        {
            _unitRepository = unitRepository;
            _mediator = mediator;
        }

        #endregion

        #region Methods
        public async Task<Result<EntityUpdatedResponse>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            var updateUnits = await _unitRepository.GetByIdIntAsync(request.UnitId);

            if (updateUnits == null)
            {
                throw new InvoiceNotFoundException(request.UnitId);
            }
                updateUnits.Name = request.Name != null ? request.Name : updateUnits.Name;
                updateUnits.BuildingId = request.BuildingId > 0 ? request.BuildingId : updateUnits.BuildingId;
                updateUnits.FloorId = request.FloorId > 0 ? request.FloorId : updateUnits.FloorId;
                updateUnits.UnitTypeId = request.UnitTypeId > 0 ? request.UnitTypeId : updateUnits.UnitTypeId;
                updateUnits.OccupantTypeId = request.OccupantId > 0 ? request.OccupantId : updateUnits.OccupantTypeId;
                updateUnits.OccupancyTypeId = request.OccupancyId > 0 ? request.OccupancyId : updateUnits.OccupancyTypeId;
                updateUnits.Area = request.Area > 0 ? request.Area : updateUnits.Area;
                updateUnits.BHKType = request.Bhk > 0 ? request.Bhk : updateUnits.BHKType;
                updateUnits.EIntercom = request.EIntercom > 0 ? request.EIntercom : updateUnits.EIntercom;
                updateUnits.PhoneExtention = request.PhoneExtension > 0 ? request.PhoneExtension : updateUnits.PhoneExtention;

            _unitRepository.Update(updateUnits);

            await _mediator.Publish(new UnitUpdateEvent(updateUnits.Id), cancellationToken);
            return Result.Success(new EntityUpdatedResponse(updateUnits.Id));

        }
        #endregion


    }
}

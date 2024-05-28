using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Buildings.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Buildings.Commands.UpdateBuilding;

public class UpdateBuildingCommandHandler : ICommandHandler<UpdateBuildingCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IBuildingRepository _buildingRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public UpdateBuildingCommandHandler(IBuildingRepository buildingRepository, IMediator mediator)
    {
        _buildingRepository = buildingRepository;
        _mediator = mediator;
    }
    #endregion

    #region Method
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateBuildingCommand request, CancellationToken cancellationToken)
    {
        var building = await _buildingRepository.GetByIntIdAsync(request.BuildingId);

        if (building == null)
        {
            throw new BuildingNotFoundException(request.BuildingId);
        }

        building.Name = request.Name ?? building.Name;
        building.BuildingTypeId = request.BuildingTypeId > 0 ? request.BuildingTypeId: building.BuildingTypeId  ;
        building.TotalUnits = request.TotalUnits > 0 ? request.TotalUnits : building.TotalUnits;

        _buildingRepository.Update(building);

        await _mediator.Publish(new BuildingUpdatedEvent(building.Id), cancellationToken);

        return Result.Success(new EntityUpdatedResponse(building.Id));
    }
    #endregion
}

using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.Buildings.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Floors.Commands.UpdateFloor;
public class UpdateFloorCommandHandler : ICommandHandler<UpdateFloorCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IFloorRepository _floorRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public UpdateFloorCommandHandler(IFloorRepository floorRepository, IMediator mediator)
    {
        _floorRepository = floorRepository;
        _mediator = mediator;
    }
    #endregion

    #region Method
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateFloorCommand request, CancellationToken cancellationToken)

    {
        var floor = await _floorRepository.GetByIntIdAsync(request.FloorId);

        if (floor == null)
        {
            throw new FloorNotFoundException(request.FloorId);
        }

        floor.Name = request.Name ?? floor.Name;
        floor.BuildingId = request.BuildingId > 0 ? request.BuildingId : floor.BuildingId;
        floor.TotalUnits = request.TotalUnits > 0 ? request.TotalUnits : floor.TotalUnits;
        _floorRepository.Update(floor);

        await _mediator.Publish(new BuildingUpdatedEvent(floor.Id), cancellationToken);

        return Result.Success(new EntityUpdatedResponse(floor.Id));
    }
    #endregion
}


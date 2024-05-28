using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Buildings.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Floors;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Floors.Commands.CreateFloor;

public class CreateFloorCommandHandler : ICommandHandler<CreateFloorCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IFloorRepository _floorRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateFloorCommandHandler(IFloorRepository floorRepository, IMediator mediator)
    {
        _floorRepository = floorRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateFloorCommand request, CancellationToken cancellationToken)
    {
        int lastId = await _floorRepository.GetLastFloorIdAsync();
        int newId = lastId + 1;

        var floor = new Floor(
            newId,
            request.Name,
            request.ApartmentId,
            request.BuildingId,
            request.TotalUnits
        );

        _floorRepository.Insert(floor);
        await _mediator.Publish(new BuildingCreatedEvent(floor.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(floor.Id));
    }
    #endregion
}

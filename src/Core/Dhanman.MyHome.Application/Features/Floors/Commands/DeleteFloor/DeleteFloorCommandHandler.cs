using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Buildings.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Floors.Commands.DeleteFloor;

public class DeleteFloorCommandHandler  : ICommandHandler<DeleteFloorCommand, Result<EntityDeletedResponse>>
{
    #region Properties
    private readonly IFloorRepository _floorRepository;
    private readonly IMediator _mediator;
#endregion

    #region Constructors
public DeleteFloorCommandHandler(IFloorRepository floorRepository, IMediator mediator)
{
        _floorRepository = floorRepository;
    _mediator = mediator;
}
    #endregion

    #region Methodes
   

     async Task<Result<EntityDeletedResponse>> IRequestHandler<DeleteFloorCommand, Result<EntityDeletedResponse>>.Handle(DeleteFloorCommand request, CancellationToken cancellationToken)
    {
        var floor = await _floorRepository.GetByIntIdAsync(request.FloorId);

        if (floor == null)
        {
            throw new FloorNotFoundException(request.FloorId);
        }

        floor.IsDeleted = true;

        _floorRepository.Update(floor);

        await _mediator.Publish(new BuildingDeletedEvent(floor.Id), cancellationToken);

        return Result.Success(new EntityDeletedResponse(floor.Id));
    }
    #endregion
}
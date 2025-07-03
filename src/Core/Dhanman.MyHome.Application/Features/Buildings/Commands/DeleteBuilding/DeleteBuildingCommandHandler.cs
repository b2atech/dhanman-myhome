using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using Dhanman.MyHome.Application.Features.Buildings.Events;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Buildings.Commands.DeleteBuilding;

public class DeleteBuildingCommandHandler : ICommandHandler<DeleteBuildingCommand, Result<EntityDeletedResponse>>
{
    #region Properties
    private readonly IBuildingRepository _buildingRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public DeleteBuildingCommandHandler(IBuildingRepository buildingRepository, IMediator mediator)
    {
        _buildingRepository = buildingRepository;
        _mediator = mediator;
    }
    #endregion
    public async Task<Result<EntityDeletedResponse>> Handle(DeleteBuildingCommand request, CancellationToken cancellationToken)
    {
        var building = await _buildingRepository.GetByIntIdAsync(request.BuildingId);

        if (building == null)
        {
            throw new BuildingNotFoundException(request.BuildingId);
        }

        building.IsDeleted = true;

        _buildingRepository.Update(building);

        await _mediator.Publish(new BuildingDeletedEvent(building.Id), cancellationToken);

        return Result.Success(new EntityDeletedResponse(building.Id));
    }
}

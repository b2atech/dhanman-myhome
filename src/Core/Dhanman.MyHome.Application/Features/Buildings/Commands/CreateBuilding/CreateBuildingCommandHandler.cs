using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.Buildings.Commands.CreateBuildings;
using Dhanman.MyHome.Application.Features.Buildings.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Buildings;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Buildings.Commands.CreateBuilding;

public class CreateBuildingCommandHandler : ICommandHandler<CreateBuildingCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IBuildingRepository _buildingRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructors
    public CreateBuildingCommandHandler(IBuildingRepository buildingRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _buildingRepository = buildingRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
    {
        //int lastId = await _buildingRepository.GetLastBuildingIdAsync();
       // int newId = lastId + 1;

        var building = new Building(
            request.Name,
            request.BuildingTypeId,
            request.ApartmentId,
            request.TotalUnits
        );

        _buildingRepository.Insert(building);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new BuildingCreatedEvent(building.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(building.Id));
    }
    #endregion
}

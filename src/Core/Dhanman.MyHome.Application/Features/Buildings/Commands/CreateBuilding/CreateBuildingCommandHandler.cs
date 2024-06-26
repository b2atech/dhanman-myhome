﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
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
    #endregion

    #region Constructors
    public CreateBuildingCommandHandler(IBuildingRepository buildingRepository, IMediator mediator)
    {
        _buildingRepository = buildingRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateBuildingCommand request, CancellationToken cancellationToken)
    {
        int lastId = await _buildingRepository.GetLastBuildingIdAsync();
        int newId = lastId + 1;

        var building = new Building(
            newId,
            request.Name,
            request.BuildingTypeId,
            request.ApartmentId,
            request.TotalUnits
        );

        _buildingRepository.Insert(building);
        await _mediator.Publish(new BuildingCreatedEvent(building.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(building.Id));
    }
    #endregion
}

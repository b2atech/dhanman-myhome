﻿using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Units.Event;
using Dhanman.MyHome.Domain.Abstractions;
using MediatR;
using Unit = Dhanman.MyHome.Domain.Entities.Units.Unit;

namespace Dhanman.MyHome.Application.Features.Units.Command.CreateMultipleUnits;

public class CreateMultipleUnitCommandHandler : ICommandHandler<CreateMultipleUnitCommand, Result<List<EntityCreatedResponse>>>
{
    #region Properties
    private readonly IUnitRepository _unitRepository;
    private readonly IMediator _mediator;
    private readonly ICommonServiceClient _commonServiceClient;
    private readonly ISalesServiceClient _salesServiceClient;
    private readonly IUserContextService _userContextService;
    #endregion

    #region Constructor
    public CreateMultipleUnitCommandHandler(IUnitRepository unitRepository, IMediator mediator, ICommonServiceClient commonServiceClient, ISalesServiceClient salesServiceClient, IUserContextService userContextService)
    {
        _unitRepository = unitRepository;
        _mediator = mediator;
        _commonServiceClient = commonServiceClient;
        _salesServiceClient = salesServiceClient;
        _userContextService = userContextService;
    }
    #endregion

    #region Methodes

    public async Task<Result<List<EntityCreatedResponse>>> Handle(CreateMultipleUnitCommand request, CancellationToken cancellationToken)
    {
        var createdResponses = new List<EntityCreatedResponse>();
        int lastId = await _unitRepository.GetLastUnitIdAsync();

        foreach (var unitDto in request.UnitList)
        {
            int newId = ++lastId;


            // Check if the entity already exists
            var existingUnit = await _unitRepository.GetBydIdIntAsync(newId);
            if (existingUnit != null)
            {
                throw new InvalidOperationException($"Unit with ID {newId} already exists.");
            }
            var unit = new Unit(newId,
                unitDto.Name,
                unitDto.BuildingId,
                unitDto.FloorId,
                unitDto.UnitTypeId,
                unitDto.OccupantId,
                unitDto.OccupancyId,
                unitDto.Area,
                unitDto.Bhk,
                unitDto.PhoneExtension,
                unitDto.EIntercom,
                "1.0",
                "1.1",
                unitDto.ApartmentId,
                Guid.NewGuid()
            );

            _unitRepository.Insert(unit);

            await _commonServiceClient.CreateCustomerAsync(new Contracts.CustomerDto() { Id = unit.CustomerId, Name = unitDto.Name, CompanyId = unitDto.ApartmentId, CreatedBy = _userContextService.GetCurrentUserId() });
            await _salesServiceClient.CreateCustomerAsync(new Contracts.CustomerDto() { Id = unit.CustomerId, Name = unitDto.Name, CompanyId = unitDto.ApartmentId, CreatedBy = _userContextService.GetCurrentUserId() });

            await _mediator.Publish(new UnitCreatedEvent(unit.Id), cancellationToken);

            createdResponses.Add(new EntityCreatedResponse(unit.Id));
        }

        return Result.Success(createdResponses);
    }
    #endregion


}

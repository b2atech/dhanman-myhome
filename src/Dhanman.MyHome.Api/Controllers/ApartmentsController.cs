using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.MyHome.Application.Features.Buildings.Queries;
using Dhanman.MyHome.Application.Features.Residents.Queries;
using Dhanman.MyHome.Application.Features.Units.Queries;
using Dhanman.MyHome.Application.Features.Vehicles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ApartmentsController : ApiController
{
    public ApartmentsController(IMediator mediator) : base(mediator)
    {
    }

    #region Buildings     

    [HttpGet(ApiRoutes.Buildings.GetAllBuildings)]
    [ProducesResponseType(typeof(BuildingListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBuildings() =>
    await Result.Success(new GetAllBuildingsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Buildings.GetAllBuildingNames)]
    [ProducesResponseType(typeof(BuildingNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBuildingNames() =>
    await Result.Success(new GetAllBuildingNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region Units     

    [HttpGet(ApiRoutes.Units.GetAllUnits)]
    [ProducesResponseType(typeof(UnitListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUnits() =>
    await Result.Success(new GetAllUnitsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Units.GetAllUnitNames)]
    [ProducesResponseType(typeof(UnitNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUnitNames() =>
    await Result.Success(new GetAllUnitNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region Residents     

    [HttpGet(ApiRoutes.Residents.GetAllResidents)]
    [ProducesResponseType(typeof(ResidentListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllResidents() =>
    await Result.Success(new GetAllResidentsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Residents.GetAllResidentNames)]
    [ProducesResponseType(typeof(ResidentNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllResidentNames() =>
    await Result.Success(new GetAllResidentNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region Vehicles     

    [HttpGet(ApiRoutes.Vehicles.GetAllVehicles)]
    [ProducesResponseType(typeof(VehicleListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVehicles() =>
    await Result.Success(new GetAllVehiclesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Vehicles.GetAllVehicleNames)]
    [ProducesResponseType(typeof(VehicleNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVehicleNames() =>
    await Result.Success(new GetAllVehicleNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion
}
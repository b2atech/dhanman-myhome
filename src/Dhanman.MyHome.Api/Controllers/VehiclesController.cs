using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.MyHome.Application.Features.Vehicles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class VehiclesController : ApiController
{
    public VehiclesController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }


    #region Vehicles     
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Vehicle.Read")]
    [HttpGet(ApiRoutes.Vehicles.GetAllVehicles)]
    [ProducesResponseType(typeof(VehicleListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVehicles() =>
    await Result.Success(new GetAllVehiclesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Vehicle.Read")]
    [HttpGet(ApiRoutes.Vehicles.GetAllVehicleNames)]
    [ProducesResponseType(typeof(VehicleNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVehicleNames() =>
    await Result.Success(new GetAllVehicleNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion



}
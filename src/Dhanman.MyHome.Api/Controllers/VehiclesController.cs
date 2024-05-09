using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.MyHome.Application.Features.Vehicles.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class VehiclesController : ApiController
{
    public VehiclesController(IMediator mediator) : base(mediator)
    {
    }


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
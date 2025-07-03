using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Application.Features.Vehicles.commands.create;
using Dhanman.MyHome.Application.Features.Vehicles.Queries;
using Dhanman.MyHome.Application.Features.Visitors.Commands.CreateVisitor;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class VehiclesController : ApiController
{
    public VehiclesController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
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

    #region Visitor Vehicle

    [HttpPost(ApiRoutes.Vehicles.CreateVisitorVehicle)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateVisitor([FromBody] CreateVisitorVehicleRequest? request) =>
     await Result.Create(request, Errors.General.BadRequest)
         .Map(value => new CreateVisitorVehicleCommand(
             value.VisitorLogId,
             value.VehicleNumber,
             value.VehicleType
         ))
         .Bind(command => Mediator.Send(command))
         .Match(Ok, BadRequest);
    #endregion

}
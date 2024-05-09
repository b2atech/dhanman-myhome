using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Application.Features.Units.Command.CreateUnit;
using Dhanman.MyHome.Application.Features.Units.Command.GetUnitDetails;
using Dhanman.MyHome.Application.Features.Units.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class UnitsController : ApiController
{
    public UnitsController(IMediator mediator) : base(mediator)
    {
    }


    #region Units     

    [HttpGet(ApiRoutes.Units.GetAllUnits)]
    [ProducesResponseType(typeof(UnitListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUnits() =>
    await Result.Success(new GetAllUnitsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Units.GetAllUnitDetails)]
    [ProducesResponseType(typeof(UnitDetailListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLedgerDetails([FromBody] GetUnitDetailRequest? request) =>
    await Result.Create(request, Errors.General.BadRequest)
                .Map(value => new GetUnitDetailsCommand(
                    value.BuildingIds,
                    value.OccupancyIds
                  ))
                .Bind(command => Mediator.Send(command))
               .Match(Ok, BadRequest);
    [HttpGet(ApiRoutes.Units.GetAllUnitNames)]
    [ProducesResponseType(typeof(UnitNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUnitNames() =>
    await Result.Success(new GetAllUnitNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    [HttpPost(ApiRoutes.Units.CreateUnit)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateUnitListRequest([FromBody] CreateUnitListRequest? request) =>
            await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateUnitCommand(
                value.UnitList
               ))
             .Bind(command => Mediator.Send(command))
                   .Match(Ok, BadRequest);
    #endregion


}
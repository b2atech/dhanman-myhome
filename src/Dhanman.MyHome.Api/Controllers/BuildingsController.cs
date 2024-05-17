using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Application.Features.Buildings.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class BuildingsController : ApiController
{
    public BuildingsController(IMediator mediator) : base(mediator)
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

    [HttpGet(ApiRoutes.Buildings.GetAllBuildingName)]
    [ProducesResponseType(typeof(BuildingNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBuildingName(Guid apartmentId) =>
    await Result.Success(new GetAllBuildingNameQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion


}
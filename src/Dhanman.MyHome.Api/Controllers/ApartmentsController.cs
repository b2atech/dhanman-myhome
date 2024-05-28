using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Apartments;
using Dhanman.MyHome.Application.Contracts.Floors;
using Dhanman.MyHome.Application.Contracts.Gates;
using Dhanman.MyHome.Application.Contracts.OccupancyTypes;
using Dhanman.MyHome.Application.Contracts.OccupantTypes;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Application.Features.Apartments.Queries;
using Dhanman.MyHome.Application.Features.Floors.Queries;
using Dhanman.MyHome.Application.Features.Gates.Queries;
using Dhanman.MyHome.Application.Features.OccupancyTypes.Queries;
using Dhanman.MyHome.Application.Features.OccupantTypes.Queries;
using Dhanman.MyHome.Application.Features.Visitors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ApartmentsController : ApiController
{
    public ApartmentsController(IMediator mediator) : base(mediator)
    {
    }

    #region Apartments     

    [HttpGet(ApiRoutes.Apartments.GetApartments)]
    [ProducesResponseType(typeof(ApartmentListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApartments() =>
    await Result.Success(new GetAllApartmentsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Apartments.GetApartmentNames)]
    [ProducesResponseType(typeof(ApartmentNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApartmentNames() =>
    await Result.Success(new GetAllApartmentNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region Floors

    [HttpGet(ApiRoutes.Floors.GetFloors)]
    [ProducesResponseType(typeof(FloorListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllFloors(int buildingId) =>
    await Result.Success(new GetAllFloorsQuery(buildingId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Floors.GetFloorNames)]
    [ProducesResponseType(typeof(FloorNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllFloorNames(Guid apartmentId, int buildingId) =>
    await Result.Success(new GetAllFloorNamesQuery(apartmentId, buildingId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    #endregion

    #region Gates

    [HttpGet(ApiRoutes.Gates.GetGates)]
    [ProducesResponseType(typeof(GateListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllGates() =>
    await Result.Success(new GetAllGatesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Gates.GetGateNames)]
    [ProducesResponseType(typeof(GateNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllGateNames() =>
    await Result.Success(new GetAllGateNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region OccupancyTypes     

    [HttpGet(ApiRoutes.OccupancyTypes.GetAllOccupancyTypes)]
    [ProducesResponseType(typeof(OccupancyTypeListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllOccupancyTypes() =>
    await Result.Success(new GetAllOccupancyTypesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    #endregion

    #region Visitors   
     
    [HttpGet(ApiRoutes.Visitors.GetAllVisitors)]
    [ProducesResponseType(typeof(VisitorListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVisitors() =>
    await Result.Success(new GetAllVisitorsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Visitors.GetAllVisitorNames)]
    [ProducesResponseType(typeof(VisitorNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVisitorNames() =>
    await Result.Success(new GetAllVisitorNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region OccupantTypes

    [HttpGet(ApiRoutes.OccupantTypes.GetAllOccupantTypes)]
    [ProducesResponseType(typeof(OccupantTypeListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetAllOccupantTypes() =>
     await Result.Success(new GetAllOccupantTypesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

}
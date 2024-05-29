using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Apartments;
using Dhanman.MyHome.Application.Contracts.BuildingTypes;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Floors;
using Dhanman.MyHome.Application.Contracts.Gates;
using Dhanman.MyHome.Application.Contracts.OccupancyTypes;
using Dhanman.MyHome.Application.Contracts.OccupantTypes;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Application.Features.Apartments.Queries;
using Dhanman.MyHome.Application.Features.BuildingTypes.Queries;
using Dhanman.MyHome.Application.Features.Floors.Commands.CreateFloor;
using Dhanman.MyHome.Application.Features.Floors.Queries;
using Dhanman.MyHome.Application.Features.Gates.Commands.CreateGate;
using Dhanman.MyHome.Application.Features.Gates.Commands.DeleteGate;
using Dhanman.MyHome.Application.Features.Gates.Commands.UpdateGate;
using Dhanman.MyHome.Application.Features.Gates.Queries;
using Dhanman.MyHome.Application.Features.OccupancyTypes.Queries;
using Dhanman.MyHome.Application.Features.OccupantTypes.Queries;
using Dhanman.MyHome.Application.Features.Visitors.Queries;
using Dhanman.MyHome.Domain;
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
    public async Task<IActionResult> GetAllFloors(Guid apartmentId,int buildingId) =>
    await Result.Success(new GetAllFloorsQuery(apartmentId,buildingId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Floors.GetFloorNames)]
    [ProducesResponseType(typeof(FloorNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllFloorNames(Guid apartmentId, int buildingId) =>
    await Result.Success(new GetAllFloorNamesQuery(apartmentId, buildingId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Floors.CreateFloor)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateFloor([FromBody] CreateFloorRequest? request) =>
     await Result.Create(request, Errors.General.BadRequest)
         .Map(value => new CreateFloorCommand(
             value.Name,
             value.ApartmentId,
             value.BuildingId,
             value.TotalUnits
         ))
         .Bind(command => Mediator.Send(command))
         .Match(Ok, BadRequest);

    #endregion

    #region Gates

    [HttpGet(ApiRoutes.Gates.GetGates)]
    [ProducesResponseType(typeof(GateListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllGates(Guid apartmentId) =>
    await Result.Success(new GetAllGatesQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Gates.GetGateNames)]
    [ProducesResponseType(typeof(GateNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllGateNames(Guid apartmentId) =>
    await Result.Success(new GetAllGateNamesQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Gates.CreateGate)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateGate([FromBody] CreateGateRequest? request) =>
     await Result.Create(request, Errors.General.BadRequest)
         .Map(value => new CreateGateCommand(
             value.Name,
             value.ApartmentId,
             value.BuildingId,
             value.GateTypeId,
             value.IsUsedForIn,
             value.IsUsedForOut,
             value.IsAllUsersAllowed,
             value.IsResidentsAllowed,
             value.IsStaffAllowed,
             value.IsVendorAllowed
         ))
         .Bind(command => Mediator.Send(command))
         .Match(Ok, BadRequest);
    [HttpPut(ApiRoutes.Gates.UpdateGates)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGates([FromBody] UpdateGateRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateGateCommand(
                value.GateId,
                value.Name,
                value.ApartmentId,
                value.BuildingId,
                value.GateTypeId,
                value.IsUsedForIn,
                value.IsUsedForOut,
                value.IsAllUsersAllowed,
                value.IsResidentsAllowed,
                value.IsStaffAllowed,
                value.IsVendorAllowed
                ))
            .Bind(command => Mediator.Send(command));

        if (result.IsSuccess)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(result.Error);
        }
    }

    [HttpDelete(ApiRoutes.Gates.DeleteGateById)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGateById(int id)
    {
        var result = await Result.Success(new DeleteGateCommand(id))
                    .Bind(command => Mediator.Send(command));

        if (result.IsSuccess)
        {
            return NoContent();
        }
        else
        {
            return BadRequest(result.Error);
        }
    }
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

    #region BuildingType

    [HttpGet(ApiRoutes.BuildingsTypes.GetAllBuildingTypes)]
    [ProducesResponseType(typeof(BuildingTypeListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBuildingTypes() =>
    await Result.Success(new GetAllBuildingTypesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion
}
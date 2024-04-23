using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Apartments;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.ResidentRequests;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.MyHome.Application.Features.Apartments.Queries;
using Dhanman.MyHome.Application.Features.Buildings.Queries;
using Dhanman.MyHome.Application.Contracts.Floors;
using Dhanman.MyHome.Application.Contracts.Gates;
using Dhanman.MyHome.Application.Features.ResidentRequests.Commands.CreateResidentRequest;
using Dhanman.MyHome.Application.Features.ResidentRequests.Commands.UpdateRequestApproveStatus;
using Dhanman.MyHome.Application.Features.ResidentRequests.Commands.UpdateRequestRejectStatus;
using Dhanman.MyHome.Application.Features.ResidentRequests.Queries;
using Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;
using Dhanman.MyHome.Application.Features.Residents.Queries;
using Dhanman.MyHome.Application.Features.Units.Command.CreateUnit;
using Dhanman.MyHome.Application.Features.Units.Queries;
using Dhanman.MyHome.Application.Features.Vehicles.Queries;
using Dhanman.MyHome.Application.Features.Floors.Queries;
using Dhanman.MyHome.Application.Features.Gates.Queries;

using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Dhanman.MyHome.Application.Contracts.Floors;
using Dhanman.MyHome.Application.Contracts.Gates;
using Dhanman.MyHome.Application.Contracts.OccupancyTypes;
using Dhanman.MyHome.Application.Features.OccupancyTypes.Queries;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Application.Features.Visitors.Queries;

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

    #region Residents  

    [HttpPost(ApiRoutes.Residents.CreateResident)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateResidentRequest([FromBody] CreateResidentRequest? request) =>
            await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateResidentCommand(                
                value.UnitId,
                value.FirstName,
                value.LastName,
                value.Email,
                value.ContactNumber,
                value.PermanentAddressId,                
                value.ResidentTypeId,
                value.OccupancyStatusId,
                value.CreatedBy))
             .Bind(command => Mediator.Send(command))
                   .Match(Ok, BadRequest);

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

    #region ResidentRequests     

    [HttpPost(ApiRoutes.ResidentRequests.CreateResidentRequest)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateResidentRequest([FromBody] CreateResidentRequestRequest? request) =>
            await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateResidentRequestCommand(
                value.ApartmentId,
                value.BuildingId,
                value.FloorId,
                value.UnitId,
                value.FirstName,
                value.LastName,
                value.Email,
                value.ContactNumber,
                value.PermanentAddressId,
                value.RequestStatusId,
                value.ResidentTypeId,
                value.OccupancyStatusId,
                value.CreatedBy))
             .Bind(command => Mediator.Send(command))
                   .Match(Ok, BadRequest);

    [HttpGet(ApiRoutes.ResidentRequests.GetAllResidentRequests)]
    [ProducesResponseType(typeof(ResidentRequestListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllResidentRequests() =>
    await Result.Success(new GetAllResidentRequestsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPut(ApiRoutes.ResidentRequests.UpdateRequestApproveStatus)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRequestApproveStatus([FromBody] UpdateRequestApproveStatusRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateRequestApproveStatusCommand(
                value.Id))
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

    [HttpPut(ApiRoutes.ResidentRequests.UpdateRequestRejectStatus)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateRequestRejectStatus([FromBody] UpdateRequestRejectStatusRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateRequestRejectStatusCommand(
                value.Id))
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
    public async Task<IActionResult> GetAllFloorNames(int buildingId) =>
    await Result.Success(new GetAllFloorNamesQuery(buildingId))
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
}
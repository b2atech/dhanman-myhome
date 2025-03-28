using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Apartments;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Floors;
using Dhanman.MyHome.Application.Contracts.MemberRequests;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Application.Features.Apartments.Queries;
using Dhanman.MyHome.Application.Features.Buildings.Queries;
using Dhanman.MyHome.Application.Features.Floors.Queries;
using Dhanman.MyHome.Application.Features.MemberRequests.Commands.CreateMemberRequest;
using Dhanman.MyHome.Application.Features.MemberRequests.Queries;
using Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;
using Dhanman.MyHome.Application.Features.Units.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class PublicController : PublicApiController
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

        public PublicController(HttpClient httpClient, IConfiguration configuration, IMediator mediator) : base(mediator)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    #region ApartmentsNames

    [HttpGet(ApiRoutes.PublicApartments.GetApartmentNames)]
    [ProducesResponseType(typeof(ApartmentNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllApartmentNames() =>
   await Result.Success(new GetAllApartmentNamesQuery())
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);

    #endregion

    #region MemberRequests  
    [HttpPost(ApiRoutes.PublicMemberRequests.CreateMemberRequest)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateMemberRequest([FromBody] CreateMemberRequestRequest? request) =>
            await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateMemberRequestCommand(
                 value.ApartmentId,
                 value.FirstName,
                 value.LastName,
                 value.Email,
                 value.MobileNumber,
                 value.MemberAdditionalDetails,
                 value.CurrentAddress
                 ))
             .Bind(command => Mediator.Send(command))
                   .Match(Ok, BadRequest);

    [HttpGet(ApiRoutes.PublicMemberRequests.GetAllMemberRequests)]
    [ProducesResponseType(typeof(MemberRequestListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllMemberRequests(Guid apartmentId) =>
    await Result.Success(new GetAllMemberRequestsQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    
    #endregion



    [HttpGet(ApiRoutes.Buildings.GetAllBuildingNames)]
    [ProducesResponseType(typeof(BuildingNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBuildingNames(Guid apartmentId) =>
    await Result.Success(new GetAllBuildingNameQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    [HttpGet(ApiRoutes.Floors.GetFloorNames)]
    [ProducesResponseType(typeof(FloorNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllFloorNames(Guid apartmentId, int buildingId) =>
   await Result.Success(new GetAllFloorNamesQuery(apartmentId, buildingId))
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);


    [HttpGet(ApiRoutes.Units.GetAllUnitNames)]
    [ProducesResponseType(typeof(UnitNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUnitNames(Guid apartmentId, int buildingId, int floorId) =>
   await Result.Success(new GetAllUnitNamesQuery(apartmentId, buildingId, floorId))
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Residents.CreateResident)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateResidentRequest([FromBody] CreateResidentRequest? request) =>
           await Result.Create(request, Errors.General.BadRequest)
           .Map(value => new CreateResidentCommand(
               value.ApartmentId,
               value.UnitId,
               value.FirstName,
               value.LastName,
               value.Email,
               value.ContactNumber,
               value.PermanentAddress,
               value.ResidentTypeId,
               value.OccupancyStatusId))
            .Bind(command => Mediator.Send(command))
                  .Match(Ok, BadRequest);
}

using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.MemberRequests;
using Dhanman.MyHome.Application.Contracts.OccupantTypes;
using Dhanman.MyHome.Application.Contracts.ResidentRequests;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Features.MemberRequests.Commands.UpdateMemberApproveStatus;
using Dhanman.MyHome.Application.Features.MemberRequests.Queries;
using Dhanman.MyHome.Application.Features.OccupantTypes.Queries;
using Dhanman.MyHome.Application.Features.ResidentRequests.Commands.UpdateRequestApproveStatus;
using Dhanman.MyHome.Application.Features.ResidentRequests.Commands.UpdateRequestRejectStatus;
using Dhanman.MyHome.Application.Features.ResidentRequests.Queries;
using Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;
using Dhanman.MyHome.Application.Features.Residents.Queries;
using Dhanman.MyHome.Domain;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ResidentsController : ApiController
{
    public ResidentsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    #region Residents  
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Resident.Write")]
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

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Resident.Read")]
    [HttpGet(ApiRoutes.Residents.GetAllResidents)]
    [ProducesResponseType(typeof(ResidentListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllResidents(Guid apartmentId) =>
    await Result.Success(new GetAllResidentsQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Resident.Read")]
    [HttpGet(ApiRoutes.Residents.GetAllResidentNames)]
    [ProducesResponseType(typeof(ResidentNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllResidentNames(Guid apartmentId, [FromQuery] bool isGetAll) =>
    await Result.Success(new GetAllResidentNamesQuery(apartmentId, isGetAll))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region ResidentRequests  

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.ResidentRequest.Read")]
    [HttpGet(ApiRoutes.ResidentRequests.GetAllResidentRequests)]
    [ProducesResponseType(typeof(ResidentRequestListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllResidentRequests(Guid apartmentId) =>
    await Result.Success(new GetAllResidentRequestsQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.ResidentRequest.Approve")]
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


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.ResidentRequest.Approve")]
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

    #region MembertRequests  
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.MemberRequests.GetAllMemberRequests)]
    [ProducesResponseType(typeof(MemberRequestListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllMemberRequests(Guid apartmentId) =>
    await Result.Success(new GetAllMemberRequestsQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.ResidentRequest.Approve")]
    [HttpPut(ApiRoutes.MemberRequests.ApproveMemberRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMemberApproveStatus([FromBody] UpdateMemberApproveStatusRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateMemberApproveStatusCommand(
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

    #region OccupantTypes

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.OccupantTypes.GetAllOccupantTypes)]
    [ProducesResponseType(typeof(OccupantTypeListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetAllOccupantTypes() =>
     await Result.Success(new GetAllOccupantTypesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion
}
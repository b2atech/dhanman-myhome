using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Application.Contracts.VisitorLogs;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.UpdateVisitorApproval;
using Dhanman.MyHome.Application.Features.VisitorApprovals.Queries;
using Dhanman.MyHome.Application.Features.VisitorLogs.Commands.CreateVisitorLog;
using Dhanman.MyHome.Application.Features.VisitorLogs.Commands.UpdateVisiotLog;
using Dhanman.MyHome.Application.Features.VisitorLogs.Queries;
using Dhanman.MyHome.Application.Features.Visitors.Commands.CreateVisitor;
using Dhanman.MyHome.Application.Features.Visitors.Commands.CreateVisitorAndLog;
using Dhanman.MyHome.Application.Features.Visitors.Commands.DeleteVisitor;
using Dhanman.MyHome.Application.Features.Visitors.Commands.UpdateVisitor;
using Dhanman.MyHome.Application.Features.Visitors.Queries;
using Dhanman.MyHome.Domain;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class VisitorsController : ApiController
{
    public VisitorsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    #region Visitors   
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Visitor.Read")]
    [HttpGet(ApiRoutes.Visitors.GetAllVisitors)]
    [ProducesResponseType(typeof(VisitorListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVisitors(Guid apartmentId) =>
    await Result.Success(new GetAllVisitorsQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Visitor.Read")]
    [HttpGet(ApiRoutes.Visitors.GetAllVisitorNames)]
    [ProducesResponseType(typeof(VisitorNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVisitorNames(Guid apartmentId) =>
    await Result.Success(new GetAllVisitorNamesQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Visitor.Write")]
    [HttpPost(ApiRoutes.Visitors.CreateVisitor)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateVisitor([FromBody] CreateVisitorRequest? request) =>
     await Result.Create(request, Errors.General.BadRequest)
         .Map(value => new CreateVisitorCommand(
             value.ApartmentId,
             value.FirstName,
             value.LastName,
             value.Email,
             value.VisitingFrom,
             value.ContactNumber,
             value.VisitorTypeId,
             value.VehicleNumber,
             value.IdentityTypeId,
             value.IdentityNumber
         ))
         .Bind(command => Mediator.Send(command))
         .Match(Ok, BadRequest);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Write")]
    [HttpPost(ApiRoutes.Visitors.CreateVisitorWithPendingApproval)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateVisitorPending([FromBody] CreateVisitorWithPendingApprovalRequest? request) =>
            await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateVisitorWithPendingApprovalCommand(
                value.ApartmentId,
                value.FirstName,
                value.LastName,
                value.ContactNumber,
                value.VisitorTypeId,
                value.CreatedBy,
                value.UnitIds
                ))
             .Bind(command => Mediator.Send(command))
                   .Match(Ok, BadRequest);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Visitor.Write")]
    [HttpPut(ApiRoutes.Visitors.UpdateVisitor)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateVisitor([FromBody] UpdateVisitorRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateVisitorCommand(
                 value.Id,
                 value.ApartmentId,
                 value.FirstName,
                 value.LastName,
                 value.Email,
                 value.VisitingFrom,
                 value.ContactNumber,
                 value.VisitorTypeId,
                 value.VehicleNumber,
                 value.IdentityTypeId,
                 value.IdentityNumber
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

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Visitor.Delete")]
    [HttpDelete(ApiRoutes.Visitors.DeleteVisitorById)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteVisitorById(int id)
    {
        var result = await Result.Success(new DeleteVisitorCommand(id))
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
    [RequiresPermissions("Dhanman.MyHome.Visitor.Read")]
    [HttpGet(ApiRoutes.Visitors.GetVisitorsByUnitId)]
    [ProducesResponseType(typeof(VisitorsByUnitIdListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVisitorsByUnitId(Guid apartmentId, int unitId) =>
    await Result.Success(new GetVisitorsByUnitIdQuery(apartmentId, unitId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Visitor.Read")]
    [HttpGet(ApiRoutes.Visitors.GetVisitorsByEmailOrContactNumber)]
    [ProducesResponseType(typeof(VisitorByContactListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVisitorByContact(Guid apartmentId,[FromQuery] string contactNumber, [FromQuery] string email)
    {
        return await Result.Success(new GetVisitorByContactQuery(apartmentId, contactNumber,email))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);
    }
    #endregion

    #region Visitor Approval Actions

    //[Authorize(Policy = "DynamicPermissionPolicy")]
    //[RequiresPermissions("Dhanman.MyHome.Basic.Write")]
    [HttpPost(ApiRoutes.Visitors.VisitorsApproved)]
    [ProducesResponseType(typeof(UpdateVisitorStatusResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ApproveVisitors([FromBody] UpdateVisitorApprovalActionRequest? request) =>
       await Result.Create(request, Errors.General.BadRequest)
           .Map(value => new UpdateVisitorStatusCommand(
               value.VisitorLogId,
               value.UnitId,
               VisitorStatus.APPROVED,
               value.ModifiedBy
           ))
           .Bind(command => Mediator.Send(command))
           .Match(
               onSuccess: Ok,
               onFailure: error => error == Errors.General.EntityNotFound
                   ? NotFound(error)
                   : BadRequest(error)
           );


    //[Authorize(Policy = "DynamicPermissionPolicy")]
    //[RequiresPermissions("Dhanman.MyHome.Basic.Write")]
    [HttpPost(ApiRoutes.Visitors.VisitorsReject)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RejectVisitors([FromBody] UpdateVisitorApprovalActionRequest? request) =>
          await Result.Create(request, Errors.General.BadRequest)
          .Map(value => new UpdateVisitorStatusCommand(
              value.VisitorLogId,
              value.UnitId,
              VisitorStatus.REJECTED,
              value.ModifiedBy
              ))
           .Bind(command => Mediator.Send(command))
                 .Match(Ok, BadRequest);
    #endregion

    #region VisitorLogs 

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Visitor.Read")]
    [HttpGet(ApiRoutes.Visitors.GetAllVisitorLogs)]
    [ProducesResponseType(typeof(AllVisitorLogListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVisitorLogs(Guid apartmentId,DateTime date) =>
   await Result.Success(new GetAllVisitorLogsQuery(apartmentId, date))
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Visitor.Read")]
    [HttpGet(ApiRoutes.Visitors.GetSingleVisitorLogs)]
    [ProducesResponseType(typeof(VisitorLogListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSingleVisitorLogs(Guid apartmentId, int visitorId, int visitorTypeId) =>
    await Result.Success(new GetSingleVisitorLogsQuery(apartmentId, visitorId, visitorTypeId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Visitor.Write")]
    [HttpPost(ApiRoutes.Visitors.CheckInVisitorLog)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateVisitorLog([FromBody] CreateVisitorLogRequest? request) =>
        await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateVisitorLogCommand(
                value.VisitorId,
                value.VisitingUnitIds,
                value.VisitorTypeId,
                value.VisitingFrom,
                value.EntryTime,
                value.ExitTime,
                value.VisitorStatusId))
            .Bind(command => Mediator.Send(command))
           .Match(Ok, BadRequest);
  


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Visitor.Write")]
    [HttpPut(ApiRoutes.Visitors.CheckOutVisitorLog)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateVisitorLog([FromBody] UpdateVisitorLogRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateVisitorLogCommand(
                 value.Ids
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


    #endregion

    #region Visitor Types
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.Visitors.GetVisitorTypes)]
    [ProducesResponseType(typeof(VisitorTypeListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVisitorTypes() =>
    await Result.Success(new GetVisitorTypesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    #endregion

    #region Visitor Identity Types
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.Visitors.GetVisitorIdentityTypes)]
    [ProducesResponseType(typeof(VisitorIdentityTypeListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVisitorIdentityTypes() =>
    await Result.Success(new GetVisitorIdentityTypesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    #endregion

    #region Visitor Approval Info
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.Visitors.GetVisitorApprovalInfoById)]
    [ProducesResponseType(typeof(VisitorApprovalsInfoByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVisitorApprovalInfoById(int visitorApprovalId) =>
    await Result.Success(new GetVisitorApprovalInfoByIdQuery(visitorApprovalId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    #endregion
}

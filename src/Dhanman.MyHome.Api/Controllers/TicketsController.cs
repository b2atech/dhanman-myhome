using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.TicketCategories;
using Dhanman.MyHome.Application.Contracts.TicketPriorities;
using Dhanman.MyHome.Application.Contracts.Tickets;
using Dhanman.MyHome.Application.Contracts.TicketServiceProviderOtps;
using Dhanman.MyHome.Application.Contracts.TicketStatuses;
using Dhanman.MyHome.Application.Features.Tickets.Commands.CreateTicket;
using Dhanman.MyHome.Application.Features.Tickets.Commands.UpdateTicket;
using Dhanman.MyHome.Application.Features.Tickets.Queries;
using Dhanman.MyHome.Application.Features.TicketServiceProviderOtps.Commands.CreateTicketServiceProviderOtp;
using Dhanman.MyHome.Application.Features.TicketServiceProviderOtps.Queries;
using Dhanman.MyHome.Application.Features.TicketStatuses.Commands.UpdateTicketNextStatus;
using Dhanman.MyHome.Domain;
using Dhanman.Shared.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class TicketsController : ApiController
{
    public TicketsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    #region Tickets

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Ticket.Write")]
    [HttpPost(ApiRoutes.Tickets.CreateTicket)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateFloor([FromBody] CreateTicketRequest? request) =>
     await Result.Create(request, Errors.General.BadRequest)
         .Map(value => new CreateTicketCommand(
             value.Id,
             value.ApartmentId,
             value.UnitId,
             value.Title,
             value.Description,
             value.TicketCategoryId,
             value.TicketPriorityId,
             value.TicketStatusId,
             value.TicketAssignedTo
         ))
         .Bind(command => Mediator.Send(command))
         .Match(Ok, BadRequest);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Ticket.Read")]
    [HttpGet(ApiRoutes.Tickets.GetAllTickets)]
    [ProducesResponseType(typeof(TicketListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllTickets(Guid apartmentId) =>
    await Result.Success(new GetAllTicketsQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Ticket.Read")]
    [HttpGet(ApiRoutes.Tickets.GetTicketById)]
    [ProducesResponseType(typeof(TicketResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketById(Guid id) =>
    await Result.Success(new GetTicketByIdQuery(id))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Ticket.Write")]
    [HttpPut(ApiRoutes.Tickets.UpdateTicketServiceProvider)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTicketServiceProvider([FromBody] UpdateTicketServiceProvicerRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateTicketServiceProviderCommand(
                value.TicketId,
                value.ServiceProviderId,
                UserContextService.CurrentUserId
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
    [RequiresPermissions("Dhanman.MyHome.Ticket.Read")]
    [HttpGet(ApiRoutes.Tickets.GetAllServiceProviderTicketCategories)]
    [ProducesResponseType(typeof(ServiceProviderTicketCategoryListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllServiceProviderTicketCategories() =>
   await Result.Success(new GetAllServiceProviderTicketCategoryQuery())
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);
    #endregion

    #region Status Catetory priority 

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.TicketStatuses.GetTicketStatuses)]
    [ProducesResponseType(typeof(TicketStatusListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketStatuses() =>
    await Result.Success(new GetTicketStatusesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.TicketCategories.GetTicketCategories)]
    [ProducesResponseType(typeof(TicketCategoryListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketCategories() =>
    await Result.Success(new GetTicketCategoriesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Ticket.Read")]
    [HttpGet(ApiRoutes.TicketPriorities.GetTicketPriorities)]
    [ProducesResponseType(typeof(TicketPriorityListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketPriorities() =>
    await Result.Success(new GetTicketPrioritiesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    #endregion

    #region Ticket Status
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.TicketStatus.Assign")]
    [HttpPut(ApiRoutes.TicketStatuses.UpdateTicketStatusAssign)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTicketStatusAssign([FromBody] UpdateTicketStatusRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateTicketStatusCommand(
                value.TicketIds,
                value.ApartmentId,
                TicketStatuses.IN_PROGRESS,
                UserContextService.CurrentUserId
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
    [RequiresPermissions("Dhanman.MyHome.TicketStatus.Resolve")]
    [HttpPut(ApiRoutes.TicketStatuses.UpdateTicketStatusResolved)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTicketStatusResolved([FromBody] UpdateTicketStatusRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateTicketStatusCommand(
                value.TicketIds,
                value.ApartmentId,
                TicketStatuses.RESOLVED,
                UserContextService.CurrentUserId
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
    [RequiresPermissions("Dhanman.MyHome.TicketStatus.Close")]
    [HttpPut(ApiRoutes.TicketStatuses.UpdateTicketStatusClosed)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTicketStatusClosed([FromBody] UpdateTicketStatusRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateTicketStatusCommand(
                value.TicketIds,
                value.ApartmentId,
                TicketStatuses.CLOSED,
                UserContextService.CurrentUserId
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
    [RequiresPermissions("Dhanman.MyHome.TicketStatus.Cancel")]
    [HttpPut(ApiRoutes.TicketStatuses.UpdateTicketStatusCancelled)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTicketStatusCancelled([FromBody] UpdateTicketStatusRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateTicketStatusCommand(
                value.TicketIds,
                value.ApartmentId,
                TicketStatuses.RESOLVED,
                UserContextService.CurrentUserId
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
    [RequiresPermissions("Dhanman.MyHome.TicketStatus.Reject")]
    [HttpPut(ApiRoutes.TicketStatuses.UpdateTicketStatusRejected)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTicketStatusRejected([FromBody] UpdateTicketStatusRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new UpdateTicketStatusCommand(
                value.TicketIds,
                value.ApartmentId,
                TicketStatuses.REJECTED,
                UserContextService.CurrentUserId
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


    #region TicketServiceProviderOtps

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Ticket.Write")]
    [HttpPost(ApiRoutes.TicketServiceProviderOtp.CreateTicketServiceProviderOtp)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateTicketServiceProviderOtp([FromBody] CreateTicketServiceProviderOtpRequest? request)
    {
        return await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateTicketServiceProviderOtpCommand(
                value.Otp,
                value.ExpirationTime,
                value.TicketId
            ))
            .Bind(command => Mediator.Send(command))
            .Match(
                result => Ok(result), 
                BadRequest 
            );
    }


    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.TicketServiceProviderOtp.GetOtpByTicketId)]
    [ProducesResponseType(typeof(TicketServiceProviderOtpResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOtpByTicket(Guid ticketId) =>
          await Result.Success(new GetOtpByTicketIdQuery(ticketId))
          .Bind(query => Mediator.Send(query))
          .Match(Ok, NotFound);

    #endregion
}

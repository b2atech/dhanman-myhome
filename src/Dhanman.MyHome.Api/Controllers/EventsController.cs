using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.BookingFacilites;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Events;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Features.BookingFacilities.Queries;
using Dhanman.MyHome.Application.Features.Events.Commands.CreateEvent;
using Dhanman.MyHome.Application.Features.Events.Commands.DeleteCommand;
using Dhanman.MyHome.Application.Features.Events.Commands.UpdateEvent;
using Dhanman.MyHome.Application.Features.Events.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Dhanman.MyHome.Api.Controllers;

public class EventsController : ApiController
{
    #region Constructor
    public EventsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }
    #endregion

    #region Events
    [HttpGet(ApiRoutes.Events.GetAllEvents)]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllEvents(Guid companyId) =>
    await Result.Success(new GetAllEventsQuery(companyId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Events.GetEvent)]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEvent(Guid id) =>
    await Result.Success(new GetEventByIdQuery(id))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Events.CreateEvent)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEvents([FromBody] CreateEventRequest? request) =>
           await Result.Create(request, Errors.General.BadRequest)
           .Map(value => new CreateEventCommand(
               Guid.NewGuid(),
               value.CompanyId,
               value.CommunityCalenderId,
               value.Title,
               value.Description,
               value.StartTime,
               value.EndTime,
               value.IsRecurring,
               value.RecurrenceRule,
               value.RecurrenceRuleId,
               value.RecurrenceEndDate
               ))
            .Bind(command => Mediator.Send(command))
                  .Match(Ok, BadRequest);

    [HttpPut(ApiRoutes.Events.UpdateEvent)]
    [ProducesResponseType(typeof(EntityUpdatedResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEvents([FromBody] UpdateEventRequest? request) =>
           await Result.Create(request, Errors.General.BadRequest)
           .Map(value => new UpdateEventCommand(
               value.Id,
               value.CompanyId,
               value.CommunityCalenderId,
               value.Title,
               value.Description,
               value.StartTime,
               value.EndTime,
               value.IsRecurring,
               value.RecurrenceRule,
               value.RecurrenceRuleId,
               value.RecurrenceEndDate
               ))
            .Bind(command => Mediator.Send(command))
                  .Match(Ok, BadRequest);

    [HttpDelete(ApiRoutes.Events.DeleteEvent)]
    [ProducesResponseType(typeof(EntityDeletedResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEvent(Guid id)
    {
        var result = await Result.Success(new DeleteEventCommand(id))
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

    [HttpGet(ApiRoutes.Events.GetCalendarEvents)]
    [ProducesResponseType(typeof(EventListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCalenderEvent([FromQuery] List<int> communityCalenderIds,[FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate) =>
    await Result.Success(new GetCalendarEventsQuery(communityCalenderIds,startDate,endDate))
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);
    #endregion

    #region Bookings
    [HttpGet(ApiRoutes.BokkingFacilities.GetAllBokkingFacilities)]
    [ProducesResponseType(typeof(BookingFacilitesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBokkingFacilities() =>
    await Result.Success(new GetAllBookingFacilitesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion
}

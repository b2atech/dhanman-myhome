using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.BookingFacilites;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.EventOccurrences;
using Dhanman.MyHome.Application.Contracts.Events;
using Dhanman.MyHome.Application.Contracts.MeetingAgendaItems;
using Dhanman.MyHome.Application.Contracts.MeetingParticipants;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Contracts.Users;
using Dhanman.MyHome.Application.Features.BookingFacilities.Queries;
using Dhanman.MyHome.Application.Features.EventOccurrences.Commands.CreateEventOccurrence;
using Dhanman.MyHome.Application.Features.EventOccurrences.Commands.DeleteEventOccurrence;
using Dhanman.MyHome.Application.Features.EventOccurrences.Commands.UpdateEventOccurrence;
using Dhanman.MyHome.Application.Features.EventOccurrences.Queries;
using Dhanman.MyHome.Application.Features.Events.Commands.CreateEvent;
using Dhanman.MyHome.Application.Features.Events.Commands.DeleteCommand;
using Dhanman.MyHome.Application.Features.Events.Commands.UpdateEvent;
using Dhanman.MyHome.Application.Features.Events.Queries;
using Dhanman.MyHome.Application.Features.MeetingAgendaItems.Commands.CreateMeetingAgendaItem;
using Dhanman.MyHome.Application.Features.MeetingParticipants.Commands.UpdateMeetingParticipant;
using Dhanman.MyHome.Application.Features.MeetingParticipants.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
               value.EventTypeId,
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
               value.EventTypeId,
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
    public async Task<IActionResult> GetCalenderEvent([FromQuery] List<int> communityCalenderIds, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate) =>
    await Result.Success(new GetCalendarEventsQuery(communityCalenderIds, startDate, endDate))
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

    #region Event Occurrence

    [HttpPost(ApiRoutes.EventOccurrences.CreateEventOccurrence)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEventOccurrence([FromBody] CreateEventOccurrenceRequest? request) =>
    await Result.Create(request, Errors.General.BadRequest)
    .Map(value => new CreateEventOccurrenceCommand(
               value.EventId,
               value.OccurrenceDate,
               value.StartTime,
               value.EndTime,
               value.GeneratedFromRecurrence,
               value.EventOccurrenceStatusId,
               value.RecordingUrl,
               value.Notes,
               value.CreatedBy
               ))
            .Bind(command => Mediator.Send(command))
                  .Match(Ok, BadRequest);

    [HttpPut(ApiRoutes.EventOccurrences.UpdateEventOccurrence)]
    [ProducesResponseType(typeof(EntityUpdatedResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateEventOccurrence([FromBody] UpdateEventOccurrenceRequest? request) =>
          await Result.Create(request, Errors.General.BadRequest)
          .Map(value => new UpdateEventOccurrenceCommand(
               value.Id,
               value.EventId,
               value.OccurrenceDate,
               value.StartTime,
               value.EndTime,
               value.GeneratedFromRecurrence,
               value.EventOccurrenceStatusId,
               value.RecordingUrl,
               value.Notes
              ))
           .Bind(command => Mediator.Send(command))
                 .Match(Ok, BadRequest);

    [HttpDelete(ApiRoutes.EventOccurrences.DeleteEventOccurrenceById)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteEventOccurrenceById(int id)
    {
        var result = await Result.Success(new DeleteEventOccurrenceCommand(id))
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

    [HttpGet(ApiRoutes.EventOccurrences.GetEventOccurrence)]
    [ProducesResponseType(typeof(EventOccurrenceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEventOccurrence(int id) =>
   await Result.Success(new GetEventOccurrenceByIdQuery(id))
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);


    #endregion

    #region MeetingAgenda
    [HttpPost(ApiRoutes.MeetingAgendaItems.CreateMeetingAgendaItem)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateMeetingAgendaItem([FromBody] CreateMeetingAgendaItemRequest? request) =>
    await Result.Create(request, Errors.General.BadRequest)
    .Map(value => new CreateMeetingAgendaItemCommand(
               value.OccurrenceId,
               value.ItemText,
               value.OrderNo
               ))
            .Bind(command => Mediator.Send(command))
                  .Match(Ok, BadRequest);
    #endregion

    #region Meeting Participants
    [HttpPut(ApiRoutes.MeetingParticipants.UpdateMeetingParticipant)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMeetingParticipant([FromBody] UpdateMeetingParticipantRequest? request)
    {
        var result = await Result.Create(request, Errors.General.BadRequest)
          .Map(value => new UpdateMeetingParticipantCommand(
              value.OccurrenceId,
              value.UserIds,
              value.Role
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


    [HttpGet(ApiRoutes.MeetingParticipants.GetAllMeetingParticipants)]
    [ProducesResponseType(typeof(UserNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllMeetingParticipants(int occurrenceId) =>
       await Result.Success(new GetAllMeetingParticipantsQuery(occurrenceId))
       .Bind(query => Mediator.Send(query))
       .Match(Ok, NotFound);

    #endregion
}

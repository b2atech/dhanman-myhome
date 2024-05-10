using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.BookingFacilites;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Events;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Features.BookingFacilities.Queries;
using Dhanman.MyHome.Application.Features.Events.Commands.CreateEvent;
using Dhanman.MyHome.Application.Features.Events.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class EventsController : ApiController
{
    public EventsController(IMediator mediator) : base(mediator)
    {
    }


    #region Events
    [HttpPost(ApiRoutes.Events.CreateEvents)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEvents([FromBody] CreateEventRequest? request) =>
           await Result.Create(request, Errors.General.BadRequest)
           .Map(value => new CreateEventCommand(
               value.Name,
               value.Description,
               value.IsFullDay,
               value.BackgroundColor,
               value.TextColor,
               value.UnitId,
               value.ReservationDate,
               value.StartDate,
               value.EndDate,
               value.Pourpose,
               value.StatusId,
               value.BookingFacilitiesId
               ))
            .Bind(command => Mediator.Send(command))
                  .Match(Ok, BadRequest);

    [HttpGet(ApiRoutes.Events.GetAllEvents)]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllEventts() =>
    await Result.Success(new GetAllEventsQuery())
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

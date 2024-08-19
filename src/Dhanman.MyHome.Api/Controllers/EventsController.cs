using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class EventsController : ApiController
{
    public EventsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }


    #region Events
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Events.Write")]
    [HttpPost(ApiRoutes.Events.CreateEvents)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateEvents([FromBody] CreateEventRequest? request) =>
           await Result.Create(request, Errors.General.BadRequest)
           .Map(value => new CreateEventCommand(
               Guid.NewGuid(),
               value.Title,
               value.Description,
               value.AllDay,
               value.BackgroundColor,
               value.TextColor,
               value.ReserverdByUnitId,
               value.ReservationDate,
               value.Start,
               value.End,
               value.Pourpose,
               value.StatusId,
               value.BookingFacilitiesId
               ))
            .Bind(command => Mediator.Send(command))
                  .Match(Ok, BadRequest);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Events.Read")]
    [HttpGet(ApiRoutes.Events.GetAllEvents)]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllEventts(Guid companyId, int bookingFacilitiesId) =>
    await Result.Success(new GetAllEventsQuery(companyId, bookingFacilitiesId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

    #region Bookings
    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Events.Read")]
    [HttpGet(ApiRoutes.BokkingFacilities.GetAllBokkingFacilities)]
    [ProducesResponseType(typeof(BookingFacilitesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllBokkingFacilities() =>
  await Result.Success(new GetAllBookingFacilitesQuery())
  .Bind(query => Mediator.Send(query))
  .Match(Ok, NotFound);

    #endregion


}

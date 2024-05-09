using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Application.Features.Events.Commands.CreateEvent;
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
               value.StatusId
               ))
            .Bind(command => Mediator.Send(command))
                  .Match(Ok, BadRequest);

    #endregion



}

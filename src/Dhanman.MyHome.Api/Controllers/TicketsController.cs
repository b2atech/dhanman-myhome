using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Tickets;
using Dhanman.MyHome.Application.Features.Tickets.Commands.CreateTicket;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class TicketsController : ApiController
{
    public TicketsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    #region Tickets

    [HttpPost(ApiRoutes.Tickets.CreateTicket)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateFloor([FromBody] CreateTicketRequest? request) =>
     await Result.Create(request, Errors.General.BadRequest)
         .Map(value => new CreateTicketCommand(
             value.Id,
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
    #endregion

}

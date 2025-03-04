using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.TicketCategories;
using Dhanman.MyHome.Application.Contracts.TicketCatetories;
using Dhanman.MyHome.Application.Contracts.TicketPriorities;
using Dhanman.MyHome.Application.Contracts.Tickets;
using Dhanman.MyHome.Application.Contracts.TicketStatuses;
using Dhanman.MyHome.Application.Features.Tickets.Commands.CreateTicket;
using Dhanman.MyHome.Application.Features.Tickets.Queries;
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

    #region Status Catetory priority 
    [HttpGet(ApiRoutes.TicketStatuses.GetTicketStatuses)]
    [ProducesResponseType(typeof(TicketStatusListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketStatuses() =>
    await Result.Success(new GetTicketStatusesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.TicketCategories.GetTicketCategories)]
    [ProducesResponseType(typeof(TicketCategoryListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketCategories() =>
    await Result.Success(new GetTicketCategoriesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.TicketPriorities.GetTicketPriorities)]
    [ProducesResponseType(typeof(TicketPriorityListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTicketPriorities() =>
    await Result.Success(new GetTicketPrioritiesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    #endregion
}

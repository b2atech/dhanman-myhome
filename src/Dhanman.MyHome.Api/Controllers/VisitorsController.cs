using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Application.Features.Visitors.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class VisitorsController : ApiController
{
    public VisitorsController(IMediator mediator) : base(mediator)
    {
    }

    #region Visitors   

    [HttpGet(ApiRoutes.Visitors.GetAllVisitors)]
    [ProducesResponseType(typeof(VisitorListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVisitors() =>
    await Result.Success(new GetAllVisitorsQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpGet(ApiRoutes.Visitors.GetAllVisitorNames)]
    [ProducesResponseType(typeof(VisitorNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVisitorNames() =>
    await Result.Success(new GetAllVisitorNamesQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

}

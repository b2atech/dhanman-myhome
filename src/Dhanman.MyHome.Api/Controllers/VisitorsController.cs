using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.VisitorLogs;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Application.Features.VisitorLogs.Commands.CreateVisitorLog;
using Dhanman.MyHome.Application.Features.VisitorLogs.Queries;
using Dhanman.MyHome.Application.Features.Visitors.Commands.CreateVisitor;
using Dhanman.MyHome.Application.Features.Visitors.Commands.DeleteVisitor;
using Dhanman.MyHome.Application.Features.Visitors.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class VisitorsController : ApiController
{
    public VisitorsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
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

    [HttpPost(ApiRoutes.Visitors.CreateVisitor)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateVisitor([FromBody] CreateVisitorRequest? request) =>
     await Result.Create(request, Errors.General.BadRequest)
         .Map(value => new CreateVisitorCommand(
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

    [HttpGet(ApiRoutes.Visitors.GetVisitorsByUnitId)]
    [ProducesResponseType(typeof(VisitorsByUnitIdListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetVisitorsByUnitId(Guid apartmentId, int unitId) =>
    await Result.Success(new GetVisitorsByUnitIdQuery(apartmentId, unitId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    #endregion

    #region VisitorLogs 

    [HttpGet(ApiRoutes.Visitors.GetAllVisitorLogs)]
    [ProducesResponseType(typeof(VisitorLogListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllVisitorLogs(Guid apartmentId, int visitorId, int visitorTypeId) =>
    await Result.Success(new GetAllVisitorLogsQuery(apartmentId, visitorId, visitorTypeId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.Visitors.CreateVisitorLog)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateVisitorLog([FromBody] CreateVisitorLogRequest? request) =>
        await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateVisitorLogCommand(
                value.VisitorId,
                value.VisitingUnitIds,
                value.VisitorTypeId,
                value.VisitingFrom,
                value.CurrentStatusId,
                value.EntryTime,
                value.ExitTime))
            .Bind(command => Mediator.Send(command))
           .Match(Ok, BadRequest);
    #endregion
}

using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Complaints;
using Dhanman.MyHome.Application.Features.Events.Commands.CreateComplaint;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class ComplaintsController : ApiController
{
    public ComplaintsController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Complaint.Write")]
    [HttpPost(ApiRoutes.Complaints.CreateComplaint)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateComplaint([FromBody] CreateComplaintRequest? request) =>
        await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateComplaintCommand(
                Guid.NewGuid(),
                value.Subject,
                value.Description,
                value.DocLink,
                value.PrefferedTime,
                value.CategoryId,
                value.SubCategoryId,
                value.PriorityId,
                value.DepartmentId,
                value.OccuredDate,
                value.PrefferedDate,
                value.IsUrgent
            ))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);
}
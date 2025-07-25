using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Organizations;
using Dhanman.MyHome.Application.Features.Organizations.Commands.HardDeleteOrganization;
using Dhanman.MyHome.Application.Features.Organizations.Commands.InitializeOrganization;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dhanman.MyHome.Application.Features.Organizations.Queries;

namespace Dhanman.MyHome.Api.Controllers;

public class OrganizationController : ApiController
{
    public OrganizationController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    #region OrganizationController

    //[Authorize(Policy = "DynamicPermissionPolicy")]
    //[RequiresPermissions("Dhanman.MyHome.Write")]
    //[HttpPost(ApiRoutes.Organizations.InitializeOrganization)]
    //[ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> CreateInitializeOrganization([FromBody] CreateInitializeOrganizationRequest? request) =>
    //       await Result.Create(request, Errors.General.BadRequest)
    //           .Map(value => new InitializeOrganizationCommand(
    //                value.Id,
    //                value.Name,
    //                value.CompanyGuids,
    //                value.CompanyNames,
    //                value.UserId,
    //                value.UserFirstName,
    //                value.UserLastName,
    //                value.PhoneNumber,
    //                value.Email,
    //                UserContextService.CurrentUserId
    //               ))
    //           .Bind(command => Mediator.Send(command))
    //          .Match(Ok, BadRequest);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    [HttpGet(ApiRoutes.Organizations.GetOrganizationById)]
    [ProducesResponseType(typeof(OrganizationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOrganizationById(Guid id) =>
    await Result.Success(new GetOrganizationByIdQuery(id))
   .Bind(query => Mediator.Send(query))
   .Match(Ok, NotFound);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Delete")]
    [HttpDelete(ApiRoutes.Organizations.HardDeleteOrganization)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> HardDeleteOrganization(Guid organizationId)
    {
        var result = await Result.Success(new HardDeleteOrganizationCommand(organizationId))
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

    #endregion

}
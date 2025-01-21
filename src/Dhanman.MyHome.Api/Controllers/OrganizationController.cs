using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Organizations;
using Dhanman.MyHome.Application.Features.InitializeOrganizations.Commands.CreateInitializeOrganization;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class OrganizationController : ApiController
{
    public OrganizationController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }

    #region OrganizationController

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Write")]
    [HttpPost(ApiRoutes.Organizations.CreateInitializeOrganization)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateInitializeOrganization([FromBody] CreateInitializeOrganizationRequest? request) =>
           await Result.Create(request, Errors.General.BadRequest)
               .Map(value => new CreateInitializeOrganizationCommand(
                    Guid.NewGuid(),
                    value.Name,
                    value.CompanyGuids,
                    value.CompanyNames,
                    value.UserId,
                    value.UserFirstName,
                    value.UserLastName,
                    value.PhoneNumber,
                    value.Email,
                    UserContextService.GetCurrentUserId()
                   ))
               .Bind(command => Mediator.Send(command))
              .Match(Ok, BadRequest);

    #endregion

}
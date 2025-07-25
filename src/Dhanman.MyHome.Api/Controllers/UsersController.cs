using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Users;
using Dhanman.MyHome.Application.Features.Units.Queries;
using Dhanman.MyHome.Application.Features.Users.Commands.CreateUser;
using Dhanman.MyHome.Application.Features.Users.Queries;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class UsersController : ApiController
{
    public UsersController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }


    #region Users     

    //[Authorize(Policy = "DynamicPermissionPolicy")]
    //[RequiresPermissions("Dhanman.MyHome.Write")]
    //[HttpPost(ApiRoutes.Users.CreateUser)]
    //[ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest? request) =>
    //       await Result.Create(request, Errors.General.BadRequest)
    //           .Map(value => new CreateUserCommand(
    //               value.UserId,
    //               value.CompanyId,
    //               value.FirstName,
    //               value.LastName,
    //               value.Email,
    //               value.PhoneNumber,
    //               value.IsOwner
    //               ))
    //           .Bind(command => Mediator.Send(command))
    //          .Match(Ok, BadRequest);

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Users.read")]
    [HttpGet(ApiRoutes.Users.GetAllUsers)]
    [ProducesResponseType(typeof(UserListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllUsers() =>
    await Result.Success(new GetAllUsersQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    #endregion

}

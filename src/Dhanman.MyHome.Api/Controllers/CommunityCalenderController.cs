using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Attributes;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.CommunityCalenders;
using Dhanman.MyHome.Application.Features.CommunityCalenders.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class CommunityCalenderController : ApiController
{  
    #region Constructor
    public CommunityCalenderController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }
    #endregion

    [Authorize(Policy = "DynamicPermissionPolicy")]
    [RequiresPermissions("Dhanman.MyHome.Basic.Read")]
    #region Methods
    [HttpGet(ApiRoutes.CommunityCalenders.GetAllCommunityCalenderNames)]
    [ProducesResponseType(typeof(CommunityCalenderNameListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCommunityCalenderNames() =>
    await Result.Success(new GetAllCommunityCalenderNameQuery())
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    #endregion
}

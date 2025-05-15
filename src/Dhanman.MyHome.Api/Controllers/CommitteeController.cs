
using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.CommitteeMembers;
using Dhanman.MyHome.Application.Contracts.CommunityCalenders;
using Dhanman.MyHome.Application.Features.CommitteeMembers.Queries;
using Dhanman.MyHome.Application.Features.CommunityCalenders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Dhanman.MyHome.Api.Controllers;

public class CommitteeController : ApiController
{  
    #region Constructor
    public CommitteeController(IMediator mediator, IUserContextService userContextService) : base(mediator, userContextService)
    {
    }
    #endregion

    #region Methods
    [HttpGet(ApiRoutes.CommitteeMembers.GetAllCommitteeMembers)]
    [ProducesResponseType(typeof(CommitteeMemberResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCommitteeMembers(Guid apartmentId) =>
    await Result.Success(new GetCommitteeMembersByApartmentQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);
    #endregion
}

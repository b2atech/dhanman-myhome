
using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using B2aTech.CrossCuttingConcern.UserContext;
using Dhanman.MyHome.Api.Contracts;
using Dhanman.MyHome.Api.Infrastructure;
using Dhanman.MyHome.Application.Contracts.CommitteeMembers;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.Portfolios;
using Dhanman.MyHome.Application.Contracts.Roles;
using Dhanman.MyHome.Application.Features.CommitteeMembers.Commands;
using Dhanman.MyHome.Application.Features.CommitteeMembers.Queries;
using Dhanman.MyHome.Application.Features.Portfolios.Queries;
using Dhanman.MyHome.Application.Features.Roles;
using Dhanman.MyHome.Domain;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
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

    [HttpGet(ApiRoutes.CommitteeMembers.GetAllCommitteeMemberNames)]
    [ProducesResponseType(typeof(CommitteeMemberResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllCommitteeMembersNames(Guid apartmentId) =>
    await Result.Success(new GetCommitteeMembersByApartmentQuery(apartmentId))
    .Bind(query => Mediator.Send(query))
    .Match(Ok, NotFound);

    [HttpPost(ApiRoutes.CommitteeMembers.CreateCommitteeMember)]
    [ProducesResponseType(typeof(EntityCreatedResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCommitteeMember([FromBody] CreateCommitteeMemberRequest request) =>
        await Result.Create(request, Errors.General.BadRequest)
            .Map(value => new CreateCommitteeMemberCommand(
                userId: value.UserId,
                apartmentId: value.ApartmentId,
                effectiveStartDate: value.EffectiveStartDate,
                effectiveEndDate: value.EffectiveEndDate,
                roleId: value.RoleId,
                portfolioId: value.PortfolioId,
                createdBy:value.CreatedBy
            ))
            .Bind(command => Mediator.Send(command))
            .Match(Ok, BadRequest);

    [HttpGet(ApiRoutes.CommitteeMembers.GetAllCommitteeMembers)]
    [ProducesResponseType(typeof(CommitteeAllMemberListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCommitteeAllMembersByApartment(Guid apartmentId)
    {
        return await Result.Success(new GetCommitteeAllMembersByApartmentQuery(apartmentId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);
    }

    [HttpGet(ApiRoutes.CommitteeMembers.GetPortfolioByApartmentId)]
    [ProducesResponseType(typeof(PortfolioListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPortfoliosByApartment(Guid apartmentId)
    {
        return await Result.Success(new GetPortfoliosByApartmentQuery(apartmentId))
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);
    }

    [HttpGet(ApiRoutes.CommitteeMembers.GetAllRoles)]
    [ProducesResponseType(typeof(RoleListResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllRoles()
    {
        return await Result.Success(new GetAllRolesQuery())
            .Bind(query => Mediator.Send(query))
            .Match(Ok, NotFound);
    }
    #endregion
}

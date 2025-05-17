using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.CommitteeMembers.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.CommitteeMembers;
using MediatR;

namespace Dhanman.MyHome.Application.Features.CommitteeMembers.Commands;

public sealed class CreateCommitteeMemberCommandHandler : ICommandHandler<CreateCommitteeMemberCommand, Result<EntityCreatedResponse>>
{
    private readonly ICommitteeMemberRepository _repository;
    private readonly IUserContextService _userContextService;
    private readonly IMediator _mediator;

    public CreateCommitteeMemberCommandHandler(ICommitteeMemberRepository repository, IUserContextService userContextService, IMediator mediator)
    {
        _repository = repository;
        _userContextService = userContextService;
        _mediator = mediator;
    }

    public async Task<Result<EntityCreatedResponse>> Handle(CreateCommitteeMemberCommand request, CancellationToken cancellationToken)
    {
        var currentUsedId = _userContextService.GetCurrentUserId();

        var entity = new CommitteeMember(request.UserId, request.ApartmentId, request.EffectiveStartDate, request.EffectiveEndDate, request.RoleId, request.PortfolioId, currentUsedId);

        _repository.Insert(entity);

        await _mediator.Publish(new CommitteeMemberCreatedEvent(entity.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(entity.Id));
    }
}

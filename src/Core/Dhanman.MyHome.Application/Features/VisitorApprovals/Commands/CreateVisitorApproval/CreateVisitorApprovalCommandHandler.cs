using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.VisitorApprovals.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.VisitorApprovals;
using MediatR;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.CreateVisitorApproval;

public class CreateVisitorApprovalCommandHandler : ICommandHandler<CreateVisitorApprovalCommand, Result<EntityCreatedResponse>>
{
    #region Properties

    private readonly IMediator _mediator;
    private readonly IVisitorApprovalsRepository _visitorApprovalsRepository;

    #endregion

    #region Constructors
    public CreateVisitorApprovalCommandHandler(IVisitorApprovalsRepository visitorApprovalsRepository, IMediator mediator, IApplicationDbContext dbContext)
    {
        _mediator = mediator;
        _visitorApprovalsRepository = visitorApprovalsRepository;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorApprovalCommand request, CancellationToken cancellationToken)
    {
        VisitorApproval visitorApproval = new VisitorApproval(request.VisitorId, request.VisitTypeId, request.StartDate, request.EndDate, request.EntryTime, request.ExitTime);
        _visitorApprovalsRepository.Insert(visitorApproval);

        await _mediator.Publish(new VisitorApprovalCreatedEvent(visitorApproval.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(visitorApproval.Id));
    }

    #endregion
}

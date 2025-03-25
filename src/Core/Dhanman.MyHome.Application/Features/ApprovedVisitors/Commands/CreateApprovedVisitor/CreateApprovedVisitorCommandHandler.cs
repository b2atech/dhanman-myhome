using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ApprovedVisitors.Events;
using Dhanman.MyHome.Application.Features.Residents.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ApprovedVisitors;
using Dhanman.MyHome.Domain.Entities.Residents;
using MediatR;

namespace Dhanman.MyHome.Application.Features.ApprovedVisitors.Commands.CreateApprovedVisitor;

public class CreateApprovedVisitorCommandHandler : ICommandHandler<CreateApprovedVisitorCommand, Result<EntityCreatedResponse>>
{
    #region Properties
   
    private readonly IMediator _mediator;
    private readonly IApprovedVisitorRepository _approvedVisitorRepository;

    #endregion

    #region Constructors
    public CreateApprovedVisitorCommandHandler(IApprovedVisitorRepository approvedVisitorRepository, IMediator mediator, IApplicationDbContext dbContext)
    {
        _mediator = mediator;
        _approvedVisitorRepository = approvedVisitorRepository;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateApprovedVisitorCommand request, CancellationToken cancellationToken)
    {
        ApprovedVisitor approvedVisitor = new ApprovedVisitor(request.VisitorId, request.VisitTypeId, request.StartDate, request.EndDate, request.EntryTime, request.ExitTime);
        _approvedVisitorRepository.Insert(approvedVisitor);

        await _mediator.Publish(new ApprovedVisitorCreatedEvent(approvedVisitor.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(approvedVisitor.Id));
    }

    #endregion

}
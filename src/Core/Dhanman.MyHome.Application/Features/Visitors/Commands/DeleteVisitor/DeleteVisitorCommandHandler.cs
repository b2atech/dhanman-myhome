using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Gates.Events;
using Dhanman.MyHome.Application.Features.Visitors.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Visitors.Commands.DeleteVisitor;

public class DeleteVisitorCommandHandler : ICommandHandler<DeleteVisitorCommand, Result<EntityDeletedResponse>>
{
    #region Properties
    private readonly IVisitorRepository _visitorRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public DeleteVisitorCommandHandler(IVisitorRepository visitorRepository, IMediator mediator)
    {
        _visitorRepository = visitorRepository;
        _mediator = mediator;
    }
    #endregion
    public async Task<Result<EntityDeletedResponse>> Handle(DeleteVisitorCommand request, CancellationToken cancellationToken)
    {
        var visitor = await _visitorRepository.GetByIntIdAsync(request.VisitorId);

        if (visitor == null)
        {
            throw new VisitorNotFoundException(request.VisitorId);
        }

        visitor.IsDeleted = true;

        _visitorRepository.Update(visitor);

        await _mediator.Publish(new VisitorDeletedEvent(visitor.Id), cancellationToken);

        return Result.Success(new EntityDeletedResponse(visitor.Id));
    }
}

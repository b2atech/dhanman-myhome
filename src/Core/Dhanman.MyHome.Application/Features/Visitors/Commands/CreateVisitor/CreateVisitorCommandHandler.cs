using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Visitors.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Visitors;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Visitors.Commands.CreateVisitor;

public class CreateVisitorCommandHandler : ICommandHandler<CreateVisitorCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IVisitorRepository _visitorRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateVisitorCommandHandler(IVisitorRepository visitorRepository, IMediator mediator)
    {
        _visitorRepository = visitorRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorCommand request, CancellationToken cancellationToken)
    {
        int lastId = await _visitorRepository.GetLastVisitorIdAsync();
        int newId = lastId + 1;

        var visitor = new Visitor
        (
            newId,
            request.ApartmentId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.VisitingFrom,
            request.ContactNumber,
            request.VisitorTypeId,
            request.VehicleNumber,
            request.IdentityTypeId,
            request.IdentityNumber,
            request.EntryTime,
            request.ExitTime
            );
        _visitorRepository.Insert(visitor);
        await _mediator.Publish(new VisitorCreatedEvent(visitor.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(visitor.Id));
    }

    #endregion
}

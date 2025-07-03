using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
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
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructors
    public CreateVisitorCommandHandler(IVisitorRepository visitorRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _visitorRepository = visitorRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateVisitorCommand request, CancellationToken cancellationToken)
    {
      //  int lastId = await _visitorRepository.GetLastVisitorIdAsync();
      //  int newId = lastId + 1;

        var visitor = new Visitor
        (
            request.ApartmentId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.VisitingFrom,
            request.ContactNumber,
            request.VisitorTypeId,
            request.VehicleNumber,
            request.IdentityTypeId,
            request.IdentityNumber
            );
        _visitorRepository.Insert(visitor);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new VisitorCreatedEvent(visitor.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(visitor.Id));
    }

    #endregion
}

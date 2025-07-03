using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.Visitors.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Visitors.Commands.UpdateVisitor;

public class UpdateVisitorCommandHandler : ICommandHandler<UpdateVisitorCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IVisitorRepository _visitorRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public UpdateVisitorCommandHandler(IVisitorRepository visitorRepository, IMediator mediator)
    {
        _visitorRepository = visitorRepository;
        _mediator = mediator;
    }
    #endregion

    #region Method
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateVisitorCommand request, CancellationToken cancellationToken)
    {
        var visitor = await _visitorRepository.GetByIntIdAsync(request.VisitorId);

        if (visitor == null)
        {
            throw new VisitorNotFoundException(request.VisitorId);
        }
               
        visitor.ApartmentId = request.ApartmentId != Guid.Empty ? request.ApartmentId : visitor.ApartmentId;
        visitor.FirstName = request.FirstName ?? visitor.FirstName;
        visitor.LastName = request.LastName ?? visitor.LastName;
        visitor.Email = request.Email ?? visitor.Email;
        visitor.VisitingFrom = request.VisitingFrom ?? visitor.VisitingFrom;
        visitor.ContactNumber = request.ContactNumber ?? visitor.ContactNumber;
        visitor.VisitorTypeId = request.VisitorTypeId > 0 ? request.VisitorTypeId : visitor.VisitorTypeId;
        visitor.VehicleNumber = request.VehicleNumber ?? visitor.VehicleNumber;
        visitor.IdentityTypeId = request.IdentityTypeId > 0 ? request.IdentityTypeId : visitor.IdentityTypeId;
        visitor.IdentityNumber = request.IdentityNumber ?? visitor.IdentityNumber;

        _visitorRepository.Update(visitor);

        await _mediator.Publish(new VisitorUpdatedEvent(visitor.Id), cancellationToken);

        return Result.Success(new EntityUpdatedResponse(visitor.Id));
    }
    #endregion
}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.EventOccurrences.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.EventOccurrences.Commands.DeleteEventOccurrence;

public sealed class DeleteEventOccurrenceCommandHandler : ICommandHandler<DeleteEventOccurrenceCommand, Result<EntityDeletedResponse>>
{
    private readonly IEventOccurrenceRepository _eventOccurrenceRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteEventOccurrenceCommandHandler(IEventOccurrenceRepository eventOccurrenceRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _eventOccurrenceRepository = eventOccurrenceRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<EntityDeletedResponse>> Handle(DeleteEventOccurrenceCommand request, CancellationToken cancellationToken)
    {
        var eventOccurrence = await _eventOccurrenceRepository.GetByIntIdAsync(request.EventOccurrenceId);
        if (eventOccurrence == null)
        {
            throw new EventOccurrenceNotFoundException(request.EventOccurrenceId);
        }
        _eventOccurrenceRepository.Delete(eventOccurrence);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new EventOccurrenceDeletedEvent(eventOccurrence.Id), cancellationToken);
        return Result.Success(new EntityDeletedResponse(eventOccurrence.Id));
    }
}

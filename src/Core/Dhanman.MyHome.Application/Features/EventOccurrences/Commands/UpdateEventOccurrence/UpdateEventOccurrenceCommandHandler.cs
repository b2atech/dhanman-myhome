using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.EventOccurrences.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.EventOccurrences.Commands.UpdateEventOccurrence;

public sealed class UpdateEventOccurrenceCommandHandler : ICommandHandler<UpdateEventOccurrenceCommand, Result<EntityUpdatedResponse>>
{
    private readonly IEventOccurrenceRepository _eventOccurrenceRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateEventOccurrenceCommandHandler(IEventOccurrenceRepository eventOccurrenceRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _eventOccurrenceRepository = eventOccurrenceRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateEventOccurrenceCommand request, CancellationToken cancellationToken)
    {
        var eventOccurrence = await _eventOccurrenceRepository.GetByIntIdAsync(request.Id);

        if (eventOccurrence == null)
        {
            throw new EventOccurrenceNotFoundException(request.Id);
        }

        eventOccurrence.EventId = request.EventId;
        eventOccurrence.OccurrenceDate = request.OccurrenceDate;
        eventOccurrence.StartTime = request.StartTime;
        eventOccurrence.EndTime = request.EndTime;
        eventOccurrence.GeneratedFromRecurrence = request.GeneratedFromRecurrence;
        eventOccurrence.EventOccurrenceStatusId = request.EventOccurrenceStatusId;
        eventOccurrence.RecordingUrl = request.RecordingUrl;
        eventOccurrence.Notes = request.Notes;

        _eventOccurrenceRepository.Update(eventOccurrence);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new EventOccurrenceUpdatedEvent(eventOccurrence.Id), cancellationToken);

        return Result.Success(new EntityUpdatedResponse(eventOccurrence.Id));
    }
}
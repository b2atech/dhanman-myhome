using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.EventOccurrences.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.EventOccurrences;
using MediatR;

namespace Dhanman.MyHome.Application.Features.EventOccurrences.Commands.CreateEventOccurrence;

public sealed class CreateEventOccurrenceCommandHandler : ICommandHandler<CreateEventOccurrenceCommand, Result<EntityCreatedResponse>>
{
    private readonly IEventOccurrenceRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    public CreateEventOccurrenceCommandHandler(IEventOccurrenceRepository repository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EntityCreatedResponse>> Handle(CreateEventOccurrenceCommand request, CancellationToken cancellationToken)
    {
        var entity = new EventOccurrence(request.Id, request.EventId, request.OccurrenceDate, request.StartTime, request.EndTime, request.GeneratedFromRecurrence, request.EventOccurrenceStatusId, request.RecordingUrl, request.Notes, request.CreatedBy);

        _repository.Insert(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new EventOccurrenceCreatedEvent(entity.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(entity.Id));
    }
}
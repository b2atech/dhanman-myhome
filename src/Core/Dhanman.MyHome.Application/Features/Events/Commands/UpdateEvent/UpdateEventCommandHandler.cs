using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.Events.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Events;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Events.Commands.UpdateEvent;

public class UpdateEventCommandHandler : ICommandHandler<UpdateEventCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IEventRepository _eventRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public UpdateEventCommandHandler(IEventRepository eventRepository, IMediator mediator)
    {
        _eventRepository = eventRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
        Event existingEvent = await _eventRepository.GetBydIdAsync(request.Id);

        if (existingEvent == null)
        {
             existingEvent = new Event(
                request.Id,
                request.CompanyId,
                request.CommunityCalenderId,
                request.Title,
                request.EventTypeId,
                request.Description,
                request.StartTime,
                request.EndTime,
                request.IsRecurring,
                request.RecurrenceRule,
                request.RecurrenceRuleId,
                request.RecurrenceEndDate
            );
            _eventRepository.Insert(existingEvent);
        }
        else
        {
            existingEvent.CommunityCalenderId = request.CommunityCalenderId;
            existingEvent.Title = request.Title;
            existingEvent.EventTypeId = request.EventTypeId;
            existingEvent.Description = request.Description;
            existingEvent.StartTime = request.StartTime;
            existingEvent.EndTime = request.EndTime;
            existingEvent.IsRecurring = request.IsRecurring;
            existingEvent.RecurrenceRule = request.RecurrenceRule;
            existingEvent.RecurrenceRuleId = request.RecurrenceRuleId;
            existingEvent.RecurrenceEndDate = request.RecurrenceEndDate;

            _eventRepository.Update(existingEvent);
        }

        await _mediator.Publish(new EventCreatedEvent(existingEvent.Id), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(existingEvent.Id));
    }

    #endregion

}

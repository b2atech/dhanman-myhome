﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
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
                request.CalendarId,
                request.Title,
                request.Description,
                request.EventTypeId,
                request.StartTime,
                request.EndTime,
                request.IsRecurring,
                request.RecurrenceRuleId,
                request.Color,
                request.TextColor
            );
            _eventRepository.Insert(existingEvent);
        }
        else
        {
            existingEvent.CalenderId = request.CalendarId;
            existingEvent.Title = request.Title;
            existingEvent.Description = request.Description;
            existingEvent.EventTypeId = request.EventTypeId;
            existingEvent.StartTime = request.StartTime;
            existingEvent.EndTime = request.EndTime;
            existingEvent.IsRecurring = request.IsRecurring;
            existingEvent.RecurrenceRuleId = request.RecurrenceRuleId;
            existingEvent.Color = request.Color;
            existingEvent.TextColor = request.TextColor;

            _eventRepository.Update(existingEvent);
        }

        await _mediator.Publish(new EventCreatedEvent(existingEvent.Id), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(existingEvent.Id));
    }

    #endregion

}

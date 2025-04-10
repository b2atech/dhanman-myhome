using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants.Enums;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Events.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Events;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : ICommandHandler<CreateEventCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IEventRepository  _eventRepository;
    private readonly IApplicationDbContext  _dbContext;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateEventCommandHandler(IEventRepository eventRepository, IApplicationDbContext dbContext, IMediator mediator)
    {
        _eventRepository = eventRepository;
        _dbContext = dbContext;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = new Event(
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
        _eventRepository.Insert(eventEntity);

        if (request.IsRecurring)
        {
            var recurringEvents = GenerateRecurringEvents(request, eventEntity.Id, cancellationToken);
            _dbContext.Set<Event>().AddRangeAsync(recurringEvents);
        }

        await _mediator.Publish(new EventCreatedEvent(eventEntity.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(eventEntity.Id));
    }

    #endregion

    #region Helper Method to Generate Recurring Events
    private List<Event> GenerateRecurringEvents(CreateEventCommand request, Guid eventId, CancellationToken cancellationToken)
    {
        List<Event> events = new List<Event>();
        DateTime startDate = request.StartTime;
        DateTime endDate = request.EndTime;

        int numberOfOccurrences = request.RecurrenceRuleId switch
        {
            (int)RecurringRule.DAILY => 182,
            (int)RecurringRule.WEEKLY => 26,
            (int)RecurringRule.MONTHLY => 6,
            (int)RecurringRule.QUARTERLY => 2,
            _ => 0 
        };

        switch (request.RecurrenceRuleId)
        {
            case (int)RecurringRule.DAILY:
                for (int i = 1; i <= numberOfOccurrences; i++)
                {
                    startDate = startDate.AddDays(1);  
                    endDate = endDate.AddDays(1);
                    AddRecurringEvent(events, request, startDate, endDate);
                }
            break;

            case (int)RecurringRule.WEEKLY:
                for (int i = 1; i <= numberOfOccurrences; i++)
                {
                    startDate = startDate.AddDays(7);
                    endDate = endDate.AddDays(7);
                    AddRecurringEvent(events, request, startDate, endDate);
                }
            break;

            case (int)RecurringRule.MONTHLY:
                for (int i = 1; i <= numberOfOccurrences; i++)
                {
                    startDate = startDate.AddMonths(1);
                    endDate = endDate.AddMonths(1);
                    AddRecurringEvent(events, request, startDate, endDate);
                }
            break;

            case (int)RecurringRule.QUARTERLY:
                for (int i = 1; i <= numberOfOccurrences; i++)
                {
                    startDate = startDate.AddMonths(3);
                    endDate = endDate.AddMonths(3);
                    AddRecurringEvent(events, request, startDate, endDate);
                }
            break;

            default:
                break;
        }

        return events;
    }

    private void AddRecurringEvent(List<Event> events, CreateEventCommand request, DateTime startDate, DateTime endDate)
    {
        var recurringEvent = new Event(
            Guid.NewGuid(),
            request.CompanyId,
            request.CalendarId,
            request.Title,
            request.Description,
            request.EventTypeId,
            startDate,
            endDate,
            request.IsRecurring,
            request.RecurrenceRuleId,
            request.Color,
            request.TextColor
        );

        events.Add(recurringEvent);
    }

    #endregion
}


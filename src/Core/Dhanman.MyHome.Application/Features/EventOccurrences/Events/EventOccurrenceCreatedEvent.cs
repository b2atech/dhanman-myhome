using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.EventOccurrences.Events;

public sealed class EventOccurrenceCreatedEvent : IEvent
{
    public int EventOccurrenceId { get; }

    public EventOccurrenceCreatedEvent(int eventOccurrenceId) => EventOccurrenceId = eventOccurrenceId;
}
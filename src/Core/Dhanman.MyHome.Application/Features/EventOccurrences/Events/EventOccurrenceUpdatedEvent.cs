using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.EventOccurrences.Events;

internal class EventOccurrenceUpdatedEvent : IEvent
{
    #region Properties
    public int EventOccurrenceId { get; }
    #endregion

    #region Constructors
    public EventOccurrenceUpdatedEvent(int eventOccurrence) => EventOccurrenceId = eventOccurrence;
    #endregion
}
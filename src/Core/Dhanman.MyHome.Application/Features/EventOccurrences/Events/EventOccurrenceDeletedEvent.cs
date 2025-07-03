using Dhanman.Shared.Contracts.Abstractions.Messaging;
namespace Dhanman.MyHome.Application.Features.EventOccurrences.Events;

internal class EventOccurrenceDeletedEvent : IEvent
{
    #region Properties
    public int EventOccurrenceId { get; }
    #endregion

    #region Constructors
    public EventOccurrenceDeletedEvent(int eventOccurrence) => EventOccurrenceId = eventOccurrence;
    #endregion
}

using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Events.Events;

public sealed class EventCreatedEvent: IEvent
{
    #region Properties
    public int EventId { get; }

    #endregion

    #region Constructors
    public EventCreatedEvent(int eventId) => EventId = eventId;

    #endregion
}

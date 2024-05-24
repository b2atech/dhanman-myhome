using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Events.Events;

public sealed class EventCreatedEvent : IEvent
{
    #region Properties
    public Guid EventId { get; }

    #endregion

    #region Constructors
    public EventCreatedEvent(Guid eventId) => EventId = eventId;

    #endregion
}
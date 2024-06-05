using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Visitors.Events;

public class VisitorCreatedEvent : IEvent
{
    public int VisitorId { get; set; }

    public VisitorCreatedEvent(int gateId) => VisitorId = gateId;

}

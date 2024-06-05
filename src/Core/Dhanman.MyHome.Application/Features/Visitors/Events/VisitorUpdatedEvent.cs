using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Visitors.Events;

public class VisitorUpdatedEvent : IEvent
{
    public int VisitorId { get; set; }

    public VisitorUpdatedEvent(int gateId) => VisitorId = gateId;

}

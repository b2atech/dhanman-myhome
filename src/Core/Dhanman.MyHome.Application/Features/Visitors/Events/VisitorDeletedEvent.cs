using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Visitors.Events;

public class VisitorDeletedEvent : IEvent
{
    public int VisitorId { get; set; }

    public VisitorDeletedEvent(int gateId) => VisitorId = gateId;

}

using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Gates.Events;

public class GateCreatedEvent : IEvent
{
    public int GateId { get; set; }

    public GateCreatedEvent(int gateId) => GateId = gateId;

}
using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Gates.Events;

internal class GateUpdatedEvent : IEvent
{
    public int GateId { get; set; }

    public GateUpdatedEvent(int gateId) => GateId = gateId;

}
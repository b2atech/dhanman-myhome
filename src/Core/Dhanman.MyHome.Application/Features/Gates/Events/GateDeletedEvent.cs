using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Gates.Events;

public class GateDeletedEvent : IEvent
{
    public int GateId { get; set; }

    public GateDeletedEvent(int gateId) => GateId = gateId;

}
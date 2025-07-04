using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Floors.Events;

public class FloorCreatedEvent : IEvent
{
    public int FloorId { get; set; }

    public FloorCreatedEvent(int floorId)
    {
        FloorId = floorId;
    }
}
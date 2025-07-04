using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Buildings.Events;

public class BuildingCreatedEvent : IEvent
{
    public int BuildingId { get; set; }

    public BuildingCreatedEvent(int buildingId) => BuildingId = buildingId;
    
}

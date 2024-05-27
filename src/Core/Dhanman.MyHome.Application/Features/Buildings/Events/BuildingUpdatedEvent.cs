using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Buildings.Events;

public sealed class BuildingUpdatedEvent : IEvent
{
    #region Properties
    public int BuildingId { get; }
    #endregion

    #region Constructors
    public BuildingUpdatedEvent(int buildingId) => BuildingId = buildingId;
    #endregion
}

using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Buildings.Events;

internal class BuildingDeletedEvent : IEvent
{
    #region Properties
    public int BuildingId { get; }
    #endregion

    #region Constructors
    public BuildingDeletedEvent(int buildingId) => BuildingId = buildingId;
    #endregion
}

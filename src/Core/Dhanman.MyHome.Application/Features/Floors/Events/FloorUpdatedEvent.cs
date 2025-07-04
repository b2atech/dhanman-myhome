using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Floors.Events;

public sealed class FloorUpdatedEvent : IEvent
{
    #region Properties
    public int FloorId { get; }
    #endregion

    #region Constructors
    public FloorUpdatedEvent(int floorId) => FloorId = floorId;
    #endregion
}

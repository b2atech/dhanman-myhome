using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Event;

public sealed class WaterTankerDeliveryUpdateEvent :IEvent
{
    #region Properties
    public int WaterTankerDeliveryId { get; set; }

    #endregion

    #region Constructor
    public WaterTankerDeliveryUpdateEvent(int id) => WaterTankerDeliveryId = id;

    #endregion
}

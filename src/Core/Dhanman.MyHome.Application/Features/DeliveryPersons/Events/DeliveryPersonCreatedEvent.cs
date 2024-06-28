using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.DeliveryPersons.Events;

public sealed class DeliveryPersonCreatedEvent : IEvent
{
    #region Properties	
    public int DeliveryPersonId { get; }

    #endregion

    #region Constructors	
    public DeliveryPersonCreatedEvent(int deliveryPersonId) => DeliveryPersonId = deliveryPersonId;

    #endregion
}


	




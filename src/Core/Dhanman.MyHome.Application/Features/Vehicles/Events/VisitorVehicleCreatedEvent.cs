using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Vehicles.Events;

public class VisitorVehicleCreatedEvent : IEvent
{ 
    #region Properties
    public int VisitorVehicleId { get; }
    #endregion

    #region Constructors
    public VisitorVehicleCreatedEvent(int id) => VisitorVehicleId = id;
    #endregion
}

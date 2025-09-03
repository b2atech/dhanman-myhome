using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Vehicles.Events;

internal class VehicleCreatedEvent : IEvent
{
    #region Properties
    public int VisitorVehicleId { get; }
    #endregion

    #region Constructors
    public VehicleCreatedEvent(int id) => VisitorVehicleId = id;
    #endregion
}

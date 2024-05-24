using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.UnitServiceProviders.Events;

public class UnitServiceProviderCreatedEvent : IEvent
{
    #region Properties
    public int Id { get; }
    #endregion

    #region Constructors
    public UnitServiceProviderCreatedEvent(int id) => Id = id;
    #endregion
}

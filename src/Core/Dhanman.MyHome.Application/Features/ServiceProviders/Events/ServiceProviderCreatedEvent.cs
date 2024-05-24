using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Events;

public class ServiceProviderCreatedEvent : IEvent
{
    #region Properties
    public int Id { get; }
    #endregion

    #region Constructors
    public ServiceProviderCreatedEvent(int id) => Id = id;
    #endregion

}
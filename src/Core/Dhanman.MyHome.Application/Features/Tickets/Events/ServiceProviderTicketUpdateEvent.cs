using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Tickets.Events;

public sealed class ServiceProviderTicketUpdateEvent : IEvent
{
    #region Properties
    public Guid Id { get; set; }

    #endregion

    #region Constructor
    public ServiceProviderTicketUpdateEvent(Guid id) => Id = id;

    #endregion
}

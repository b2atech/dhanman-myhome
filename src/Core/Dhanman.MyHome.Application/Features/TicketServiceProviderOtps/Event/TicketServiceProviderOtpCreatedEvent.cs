using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.TicketServiceProviderOtps.Event;

internal class TicketServiceProviderOtpCreatedEvent : IEvent
{
    #region Properties
    public Guid UserId { get; }

    #endregion

    #region Constructors
    public TicketServiceProviderOtpCreatedEvent(Guid userId) => UserId = userId;

    #endregion
}

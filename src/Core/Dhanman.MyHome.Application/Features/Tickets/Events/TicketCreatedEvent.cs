using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Tickets.Events;

public sealed class TicketCreatedEvent : IEvent
{
    #region Properties
    public Guid ComplaintId { get; }

    #endregion

    #region Constructors
    public TicketCreatedEvent(Guid complaintId) => ComplaintId = complaintId;

    #endregion
}

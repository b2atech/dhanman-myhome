using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Complaints.Events;

public sealed class ComplaintCreatedEvent : IEvent
{
    #region Properties
    public Guid ComplaintId { get; }

    #endregion

    #region Constructors
    public ComplaintCreatedEvent(Guid complaintId) => ComplaintId = complaintId;

    #endregion
}

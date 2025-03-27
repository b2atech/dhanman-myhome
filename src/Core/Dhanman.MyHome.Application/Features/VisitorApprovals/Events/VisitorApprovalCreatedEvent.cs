using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Events;

public class VisitorApprovalCreatedEvent : IEvent
{
    #region Properties
    public int Id { get; }
    #endregion

    #region Constructors
    public VisitorApprovalCreatedEvent(int id) => Id = id;
    #endregion
}

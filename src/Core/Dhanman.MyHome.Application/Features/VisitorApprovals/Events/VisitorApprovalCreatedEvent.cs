using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Events;

public class VisitorApprovalCreatedEvent : IEvent
{
    #region Properties
    public int VisitorApproveId { get; }
    #endregion

    #region Constructors
    public VisitorApprovalCreatedEvent(int id) => VisitorApproveId = id;
    #endregion
}

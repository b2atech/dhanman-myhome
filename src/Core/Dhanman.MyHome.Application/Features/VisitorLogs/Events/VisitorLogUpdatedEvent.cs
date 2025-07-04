using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Events;

public class VisitorLogUpdatedEvent : IEvent
{
    #region Properties
    public int VisitorLogId { get; set; }
    #endregion

    #region Constructors
    public VisitorLogUpdatedEvent(int visitorLogId) => VisitorLogId = visitorLogId;
    #endregion
}

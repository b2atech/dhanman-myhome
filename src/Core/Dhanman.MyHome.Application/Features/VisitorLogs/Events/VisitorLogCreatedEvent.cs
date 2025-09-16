using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Events;

public sealed class VisitorLogCreatedEvent : IEvent
{
    #region Properties
    public int VisitorLogId { get; }

    #endregion

    #region Constructors
    public VisitorLogCreatedEvent(int visitorLogId) => VisitorLogId = visitorLogId;

    #endregion

}
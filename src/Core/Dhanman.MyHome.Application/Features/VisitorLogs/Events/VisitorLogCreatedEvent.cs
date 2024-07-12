using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Events;

public sealed class VisitorLogCreatedEvent : IEvent
{
    #region Properties
    public int VisitorId { get; }

    #endregion

    #region Constructors
    public VisitorLogCreatedEvent(int visitorId) => VisitorId = visitorId;

    #endregion

}
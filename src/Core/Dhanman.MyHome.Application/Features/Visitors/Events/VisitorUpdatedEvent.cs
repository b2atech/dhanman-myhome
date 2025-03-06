using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Visitors.Events;

public class VisitorUpdatedEvent : IEvent
{
    #region Properties
    public int VisitorId { get; set; }
    #endregion

    #region Constructors
    public VisitorUpdatedEvent(int visitorId) => VisitorId = visitorId;
    #endregion
}
 
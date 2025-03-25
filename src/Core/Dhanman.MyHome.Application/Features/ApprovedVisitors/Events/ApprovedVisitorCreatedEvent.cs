using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.ApprovedVisitors.Events;

public class ApprovedVisitorCreatedEvent : IEvent
{
    #region Properties
    public int Id { get; }
    #endregion

    #region Constructors
    public ApprovedVisitorCreatedEvent(int id) => Id = id;
    #endregion

}
using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Events;
public class MemberRequestCreatedEvent : IEvent
{
    #region Properties
    public int Id { get; }
    #endregion

    #region Constructors
    public MemberRequestCreatedEvent(int id) => Id = id;
    #endregion

}
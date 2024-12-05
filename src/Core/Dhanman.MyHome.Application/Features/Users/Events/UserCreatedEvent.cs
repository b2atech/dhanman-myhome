using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Users.Events;

internal class UserCreatedEvent : IEvent
{
    #region Properties
    public Guid UserId { get; }

    #endregion

    #region Constructors
    public UserCreatedEvent(Guid userId) => UserId = userId;

    #endregion

}

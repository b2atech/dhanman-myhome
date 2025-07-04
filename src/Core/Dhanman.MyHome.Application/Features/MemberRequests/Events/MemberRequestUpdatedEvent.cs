using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Events;

public sealed class MemberRequestUpdatedEvent : IEvent
{
    #region Properties

    // Private field
    private int requestId;

    // Public property encapsulating the field
    public int RequestId
    {
        get { return requestId; }
        private set { requestId = value; }
    }

    #endregion

    #region Constructor
    public MemberRequestUpdatedEvent(int id) => RequestId = id;

    #endregion
}
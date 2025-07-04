using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.CommitteeMembers.Events;

public sealed class CommitteeMemberCreatedEvent : IEvent
{
    public int CommitteeMemberId { get; }

    public CommitteeMemberCreatedEvent(int committeeMemberId) => CommitteeMemberId = committeeMemberId;
}

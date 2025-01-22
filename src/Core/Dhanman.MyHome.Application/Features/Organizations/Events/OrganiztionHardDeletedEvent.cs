using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Organizations.Events;

public sealed class OrganiztionHardDeletedEvent : IEvent
{
    public Guid OrganizationId { get; }

    public OrganiztionHardDeletedEvent(Guid organizationId) => OrganizationId = organizationId;
}

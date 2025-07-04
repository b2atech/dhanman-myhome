using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Organizations.Events;

public class InitializeOrganizationEvent : IEvent
{
    #region Properties

    public Guid OrganizationId { get; set; }

    #endregion

    #region Constructor
    public InitializeOrganizationEvent(Guid id) => OrganizationId = id;

    #endregion
}

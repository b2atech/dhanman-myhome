using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.InitializeOrganizations.Events;

public class InitializeOrganizationCreatedEvent : IEvent
{
    #region Properties

    public Guid OrganizationId { get; set; }

    #endregion

    #region Constructor
    public InitializeOrganizationCreatedEvent(Guid id) => OrganizationId = id;

    #endregion
}

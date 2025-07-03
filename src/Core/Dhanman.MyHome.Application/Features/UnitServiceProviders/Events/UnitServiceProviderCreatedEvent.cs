using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.UnitServiceProviders.Events;

public class UnitServiceProviderCreatedEvent : IEvent
{
    #region Properties
    public int Id { get; }

    public List<int> UnitIds { get; set; }
    #endregion

    #region Constructors
    public UnitServiceProviderCreatedEvent(int id) => Id = id;

    public UnitServiceProviderCreatedEvent(List<int> unitIds) => UnitIds = unitIds;
    
    #endregion
}

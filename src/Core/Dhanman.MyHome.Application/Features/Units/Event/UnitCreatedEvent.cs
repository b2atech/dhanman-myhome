using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Units.Event;

public class UnitCreatedEvent : IEvent
{
    #region Properties
    public int Id { get; }

    public List<int> Ids { get; }
    #endregion

    #region Constructors
    public UnitCreatedEvent(int id) => Id = id;

    public UnitCreatedEvent(List<int> ids) => Ids = ids;
    #endregion
}

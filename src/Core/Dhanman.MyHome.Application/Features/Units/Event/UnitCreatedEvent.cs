using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Units.Event;

public class UnitCreatedEvent : IEvent
{
    #region Properties
    public List<int> Ids { get; }
    #endregion

    #region Constructors
    public UnitCreatedEvent(List<int> ids) => Ids = ids;
    #endregion
}

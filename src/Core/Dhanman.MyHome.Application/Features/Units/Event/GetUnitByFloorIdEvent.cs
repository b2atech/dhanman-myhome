using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Units.Event;

internal class GetUnitByFloorIdEvent : IEvent
{
    #region Properties
    public int UnitByFloorId { get; set; }
    #endregion

    #region Constructors
    public GetUnitByFloorIdEvent(int unitByFloorId) => UnitByFloorId = unitByFloorId;
    #endregion   
}

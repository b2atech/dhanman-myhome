using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Units.Event;

public class GetUnitDetailEvent:IEvent
{
    #region Properties
    public int UnitDetaiId { get; set; }
    #endregion

    #region Constructors
    public GetUnitDetailEvent(int unitDetaiId) => UnitDetaiId = unitDetaiId;
    #endregion    
}

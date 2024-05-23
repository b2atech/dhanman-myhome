using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.UnitServiceProviders.Events;

public class GetAssignUnitsCreatedEvent: IEvent
{
    #region Properties
    public int UnitDetaiId { get; set; }
    #endregion

    #region Constructors
    public GetAssignUnitsCreatedEvent(int unitDetaiId) => UnitDetaiId = unitDetaiId;
    #endregion   
}

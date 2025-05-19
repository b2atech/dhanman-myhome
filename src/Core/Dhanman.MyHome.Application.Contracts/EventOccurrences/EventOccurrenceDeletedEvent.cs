namespace Dhanman.MyHome.Application.Contracts.EventOccurrences;

internal class EventOccurrenceDeletedEvent 
{
    #region Properties
    public int BuildingId { get; }
    #endregion

    #region Constructors
    public EventOccurrenceDeletedEvent(int buildingId) => BuildingId = buildingId;
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.VisitorLogs;

public sealed class VisitorLogResponse
{
    #region Properties
    public int Id { get; set; }
    public int VisitorId { get; set; }
    public string VisitorName { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public int? VisitorTypeId { get; set; }
    public string VisitorTypeName { get; set; }
    public string VisitingFrom { get; set; }
    public int CurrentStatusId { get; set; }
    public DateTime? EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }     
    #endregion

    #region Constructor 
    public VisitorLogResponse(int id, int visitorId, string visitorName, int unitId, string unitName, int? visitorTypeId, string visitorTypeName, string visitingFrom, int currentStatusId, DateTime? entryTime, DateTime? exitTime)
    {
        Id = id;
        VisitorId = visitorId;
        VisitorName = visitorName;
        UnitId = unitId;
        UnitName = unitName;
        VisitorTypeId = visitorTypeId;
        VisitorTypeName = visitorTypeName;
        VisitingFrom = visitingFrom;
        CurrentStatusId = currentStatusId;
        EntryTime = entryTime;
        ExitTime = exitTime;

    }    
    #endregion
}
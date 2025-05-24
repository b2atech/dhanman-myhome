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
    public DateTime? EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }   
    public int VisitorStatusId { get; set; }
    public string VisitorStatusName { get; set; }
    #endregion

    #region Constructor 
    public VisitorLogResponse(int id, int visitorId, string visitorName, int unitId, string unitName, int? visitorTypeId, string visitorTypeName, 
        string visitingFrom, DateTime? entryTime, DateTime? exitTime, int visitorStatusId, string visitorStatusName)
    {
        Id = id;
        VisitorId = visitorId;
        VisitorName = visitorName;
        UnitId = unitId;
        UnitName = unitName;
        VisitorTypeId = visitorTypeId;
        VisitorTypeName = visitorTypeName;
        VisitingFrom = visitingFrom;
        EntryTime = entryTime;
        ExitTime = exitTime;
        VisitorStatusId = visitorStatusId;
        VisitorStatusName = visitorStatusName;
    }    
    #endregion
}
namespace Dhanman.MyHome.Application.Contracts.VisitorLogs;

public class VisitorLog  
{
    #region Properties
    public int Id { get; set; }
    public int VisitorId { get; set; }
    public int VisitingUnitId { get; set; }
    public int? VisitorTypeId { get; set; }
    public string VisitingFrom { get; set; }  
    public int CurrentStatusId { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    #endregion
   

    #region Constructor
    public VisitorLog(int id, int visitorId, int visitingUnitId, int? visitorTypeId, string visitingFrom, int currentStatusId, DateTime entryTime, DateTime? exitTime)
    {
        Id = id;
        VisitorId = visitorId;
        VisitingUnitId = visitingUnitId;
        VisitorTypeId = visitorTypeId;
        VisitingFrom = visitingFrom;
        CurrentStatusId = currentStatusId;
        EntryTime = entryTime;
        ExitTime = exitTime;
    }
    #endregion
}
namespace Dhanman.MyHome.Application.Contracts.VisitorLogs;
 
public class CreateVisitorLogRequest
{
    #region Properties   
    public int VisitorId { get; set; }
    public List<int> VisitingUnitIds { get; set; }
    public int? VisitorTypeId { get; set; }
    public string VisitingFrom { get; set; }
    public int CurrentStatusId { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    public int VisitorStatusId { get; set; }
    #endregion


    #region Constructor
    public CreateVisitorLogRequest(int visitorId, List<int> visitingUnitIds, int? visitorTypeId, string visitingFrom, int currentStatusId, DateTime entryTime, 
        DateTime? exitTime, int visitorStatusId)
    {       
        VisitorId = visitorId;
        VisitingUnitIds = visitingUnitIds;
        VisitorTypeId = visitorTypeId;
        VisitingFrom = visitingFrom;
        CurrentStatusId = currentStatusId;
        EntryTime = entryTime;
        ExitTime = exitTime;
        VisitorStatusId = visitorStatusId;
    }

    #endregion
}
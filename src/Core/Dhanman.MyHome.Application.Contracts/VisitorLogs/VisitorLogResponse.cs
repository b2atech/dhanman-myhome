namespace Dhanman.MyHome.Application.Contracts.VisitorLogs;

public sealed class VisitorLogResponse
{
    #region Properties
    public int Id { get; }
    public int VisitorId { get; }
    public string VisitorName { get; }
    public int? VisitorTypeId { get; }
    public string VisitorTypeName { get; }
    public string VisitingFrom { get; }
    public int CurrentStatusId { get; }
    public DateTime EntryTime { get; }
    public DateTime? ExitTime { get; }
    #endregion

    #region Constructor 
    public VisitorLogResponse(int id, int visitorId, string visitorName, int? visitorTypeId, string visitorTypeName, string visitingFrom, int currentStatusId, DateTime entryTime, DateTime? exitTime)
    {
        Id = id;
        VisitorId = visitorId;
        VisitorName = visitorName;
        VisitorTypeId = visitorTypeId;
        VisitorTypeName = visitorTypeName;
        VisitingFrom = visitingFrom;
        CurrentStatusId = currentStatusId;
        EntryTime = entryTime;
        ExitTime = exitTime;
    }
    #endregion
}
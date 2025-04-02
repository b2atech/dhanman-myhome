namespace Dhanman.MyHome.Application.Contracts.VisitorLogs;

public class AllVisitorLogResponse
{
    public int Id { get; set; }
    public int VisitorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; }
    public DateTime LatestEntryTime { get; set; }
    public DateTime? LatestExitTime { get; set; }
    public string VisitingFrom { get; set; }
    public string ContactNumber { get; set; }
    public int VisitorTypeId { get; set; }
    public string VisitorTypeName { get; set; }

    public AllVisitorLogResponse(int id, int visitorId, string firstName, string lastName, int unitId, string unitName, DateTime latestEntryTime, DateTime? latestExitTime, string visitingFrom, string contactNumber, int visitorTypeId, string visitorTypeName)
    {
        Id = id;
        VisitorId = visitorId;
        FirstName = firstName;
        LastName = lastName;
        UnitId = unitId;
        UnitName = unitName;
        LatestEntryTime = latestEntryTime;
        LatestExitTime = latestExitTime;
        VisitingFrom = visitingFrom;
        ContactNumber = contactNumber;
        VisitorTypeId = visitorTypeId;
        VisitorTypeName = visitorTypeName;
    }
}

namespace Dhanman.MyHome.Application.Contracts.ApprovedVisitors;

public class CreateApprovedVisitorRequest
{
    #region Properties
    public int VisitorId { get; set; }
    public int VisitTypeId { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeOnly? EntryTime { get; set; }
    public TimeOnly? ExitTime { get; set; }

    #endregion

    //#region Constructors
    //public CreateApprovedVisitorRequest() => FirstName = string.Empty;
    //#endregion
}

namespace Dhanman.MyHome.Application.Contracts.VisitorApprovals;

public class CreateVisitorApprovalRequest
{
    #region Properties
    public Guid ApartmentId { get; set; }
    public string FirstName { get; set; }
    public string ContactNumber { get; set; }
    public int VisitorTypeId { get; set; }
    public int VisitTypeId { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeOnly? EntryTime { get; set; }
    public TimeOnly? ExitTime { get; set; }
    public Guid CreatedBy { get; set; }
    #endregion
}

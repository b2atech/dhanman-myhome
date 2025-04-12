namespace Dhanman.MyHome.Application.Contracts.VisitorApprovals;

public class UpadateVisitorApproveRequest
{
    public int Id { get; set; }
    public int VisitTypeId { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeOnly? EntryTime { get; set; }
    public TimeOnly? ExitTime { get; set; }
    public string? VehicleNumber { get; set; }
    public string? CompanyName { get; set; }
    public Guid CreatedBy { get; set; }
}

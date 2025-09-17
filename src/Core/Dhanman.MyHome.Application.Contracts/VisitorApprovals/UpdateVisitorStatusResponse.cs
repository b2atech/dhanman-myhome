namespace Dhanman.MyHome.Application.Contracts.VisitorApprovals;

public class UpdateVisitorStatusResponse
{
    public int VisitorLogId { get; set; }
    public int TotalVisitsCount { get; set; }
    public int ApprovedVisitsCount { get; set; }
    public int FinalStatusId { get; set; }

    public UpdateVisitorStatusResponse() { }
}
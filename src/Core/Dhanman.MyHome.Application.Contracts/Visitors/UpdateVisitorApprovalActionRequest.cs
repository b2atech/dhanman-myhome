namespace Dhanman.MyHome.Application.Contracts.Visitors;

public class UpdateVisitorApprovalActionRequest
{
    public int VisitorLogId { get; set; }
    public int UnitId { get; set; }
    public int VisitorStatusId { get; set; }
    public Guid ModifiedBy { get; set; }

}

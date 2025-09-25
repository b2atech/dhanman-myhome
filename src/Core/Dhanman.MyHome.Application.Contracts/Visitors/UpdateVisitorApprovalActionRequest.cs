namespace Dhanman.MyHome.Application.Contracts.Visitors;

public class UpdateVisitorApprovalActionRequest
{
    public int VisitorLogId { get; set; }
    public int UnitId { get; set; }
    public int VisitorStatusId { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public Guid ModifiedBy { get; set; }

}

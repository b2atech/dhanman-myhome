namespace Dhanman.MyHome.Domain.Entities.VisitorApprovals;
public class ApproveVisitorForUnitDto
{
    public int VisitorLogId { get; set; }
    public int UnitId { get; set; }
    public int ApproverResidentId { get; set; }
    public string ApproverFirstName { get; set; } = string.Empty;
    public string ApproverLastName { get; set; } = string.Empty;
    public int FinalStatusId { get; set; }
}
public class ApproveVisitorForUnitResponse : ApproveVisitorForUnitDto { }


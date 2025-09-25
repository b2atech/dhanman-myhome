using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dhanman.MyHome.Application.Contracts.VisitorApprovals;
public class VisitorApprovalActionResponse
{
    public int VisitorLogId { get; set; }
    public int UnitId { get; set; }
    public int ApproverResidentId { get; set; }
    public string ApproverFirstName { get; set; } = string.Empty;
    public string ApproverLastName { get; set; } = string.Empty;
    public int FinalStatusId { get; set; }
}

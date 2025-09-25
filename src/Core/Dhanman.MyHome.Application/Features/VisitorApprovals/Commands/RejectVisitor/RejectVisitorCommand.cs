using Dhanman.MyHome.Application.Contracts.Enums;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.UpdateVisitorApproval;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.RejectVisitor;

public sealed class RejectVisitorCommand
    : UpdateVisitorStatusCommand
{
    public RejectVisitorCommand(
        int visitorLogId,
        int unitId,
        string deviceId,
        Guid modifiedBy)
        : base(visitorLogId, unitId, VisitorStatus.REJECTED, deviceId, modifiedBy)
    {
    }
}

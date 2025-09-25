using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Contracts.Enums;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.MyHome.Application.Enums;
using Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.UpdateVisitorApproval;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.ApproveVisitor;

public sealed class ApproveVisitorCommand
    : UpdateVisitorStatusCommand
{
    public ApproveVisitorCommand(
        int visitorLogId,
        int unitId,
        string deviceId,
        Guid modifiedBy)
        : base(visitorLogId, unitId, VisitorStatus.APPROVED, deviceId, modifiedBy)
    {
    }
}
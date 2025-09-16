using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.UpdateVisitorApproval;

public sealed class UpdateVisitorApprovalActionCommand : ICommand<Result<ApprovalActionResponse>>
{
    public int VisitorLogId { get; }
    public int UnitId { get; }
    public int VisitorStatusId { get; } 
    public Guid ModifiedBy { get; }

    public UpdateVisitorApprovalActionCommand(
        int visitorLogId,
        int unitId,
        int visitorStatusId,
        Guid modifiedBy)
    {
        VisitorLogId = visitorLogId;
        UnitId = unitId;
        VisitorStatusId = visitorStatusId;
        ModifiedBy = modifiedBy;
    }
}

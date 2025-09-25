using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Contracts.Enums;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.UpdateVisitorApproval;

public  class UpdateVisitorStatusCommand : ICommand<Result<UpdateVisitorStatusResponse>>
{
    public int VisitorLogId { get; }
    public int UnitId { get; }
    public VisitorStatus VisitorStatusId { get; } 
    public string? DeviceId { get; }
    public Guid ModifiedBy { get; }

    public UpdateVisitorStatusCommand(
        int visitorLogId,
        int unitId,
        VisitorStatus visitorStatusId,
        string? deviceId,
        Guid modifiedBy)
    {
        VisitorLogId = visitorLogId;
        UnitId = unitId;
        VisitorStatusId = visitorStatusId;
        DeviceId = deviceId;
        ModifiedBy = modifiedBy;
    }
}

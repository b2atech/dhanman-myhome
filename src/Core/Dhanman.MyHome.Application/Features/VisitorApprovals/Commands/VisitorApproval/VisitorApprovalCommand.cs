using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Commands.VisitorApproval;

public class VisitorApprovalCommand : ICommand<Result<VisitorApprovalNotificationResponse>>
{
    public int VisitorLogId { get; }

    public VisitorApprovalCommand( int visitorLogId)
    {
        VisitorLogId = visitorLogId;
    }
}

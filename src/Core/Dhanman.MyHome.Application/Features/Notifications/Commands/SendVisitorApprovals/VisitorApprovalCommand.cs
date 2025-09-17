using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Notifications.Commands.SendVisitorApprovals;

public class VisitorApprovalCommand : ICommand<Result<object>>
{
    public int VisitorLogId { get; }

    public VisitorApprovalCommand( int visitorLogId)
    {
        VisitorLogId = visitorLogId;
    }
}

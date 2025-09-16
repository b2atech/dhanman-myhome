using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Notifications.Commands.SendUnitPushNotifications;

public class SendRequestApprovalActionCommand : ICommand<Result<object>>
{
    public int VisitorLogId { get; }

    public SendRequestApprovalActionCommand( int visitorLogId)
    {
        VisitorLogId = visitorLogId;
    }
}

using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Notifications.Commands.SendUnitPushNotifications;

public class SendRequestApprovalActionCommand : ICommand<Result<object>>
{
    public int UnitId { get; }
    public string GuestName { get; }
    public int GuestId { get; }

    public SendRequestApprovalActionCommand(int unitId, string guestName, int guestId)
    {
        UnitId = unitId;
        GuestName = guestName;
        GuestId = guestId;
    }
}

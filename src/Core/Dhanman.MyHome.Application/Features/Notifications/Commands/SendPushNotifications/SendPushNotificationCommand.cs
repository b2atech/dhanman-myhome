using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Notifications.Commands.SendPushNotifications;

public class SendPushNotificationCommand : ICommand<Result<object>>
{
    #region Properties
    public int ResidentId { get; set; }
    public string GuestName { get; set; }

    #endregion

    #region Constructor
    public SendPushNotificationCommand(int residentId, string guestName)
    {
        ResidentId = residentId;
        GuestName = guestName;
    }
    #endregion
}

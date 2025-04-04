namespace Dhanman.MyHome.Application.Contracts.Notifications;

public class GuestPushNotificationRequest
{
    #region Properties
    public int ResidentId { get; set; }
    public string GuestName { get; set; }
    public int GuestId { get; set; }
    #endregion

    #region Constructors
    public GuestPushNotificationRequest()
    {

    }
    #endregion

}

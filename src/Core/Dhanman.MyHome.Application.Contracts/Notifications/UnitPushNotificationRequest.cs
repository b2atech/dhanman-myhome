namespace Dhanman.MyHome.Application.Contracts.Notifications;

public class UnitPushNotificationRequest
{
    public string GuestName { get; set; }
    public int GuestId { get; set; }

    public UnitPushNotificationRequest( string guestName, int guestId)
    {
        GuestName = guestName;
        GuestId = guestId;
    }
}

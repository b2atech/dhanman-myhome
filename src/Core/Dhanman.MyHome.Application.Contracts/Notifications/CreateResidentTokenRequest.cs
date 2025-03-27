namespace Dhanman.MyHome.Application.Contracts.Notifications;

public class CreateResidentTokenRequest
{
    #region Properties
    public int ResidentId { get; set; }
    public string FCMToken { get; set; }

    #endregion

    #region Constructor
    public CreateResidentTokenRequest()
    {
        
    }
    public CreateResidentTokenRequest(int residentId, string fcmToken)
    {
        ResidentId = residentId;
        FCMToken = fcmToken;
    }
    #endregion
}

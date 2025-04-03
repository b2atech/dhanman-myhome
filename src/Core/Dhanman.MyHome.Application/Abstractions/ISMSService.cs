namespace Dhanman.MyHome.Application.Abstractions;

public interface ISMSService
{

    #region Methods
    Task<bool> SendSMS(string recipient, string welcomeMessage);
    #endregion

}

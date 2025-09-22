namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;

public class CheckinServiceProviderRequest
{
    #region Properties    
    public string Pin { get; set; }
    #endregion


    #region Constructor
    public CheckinServiceProviderRequest(string pin)
    {
        Pin = pin;
    }

    public CheckinServiceProviderRequest() { }
    #endregion
}

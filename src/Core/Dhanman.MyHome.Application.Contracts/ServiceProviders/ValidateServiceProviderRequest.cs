namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;

public class ValidateServiceProviderRequest
{

    #region Properties    
  public Guid ApartmentId { get; set; }

    public string Pin { get; set; }
    #endregion

    #region Constructor
    public ValidateServiceProviderRequest(Guid apartmentId, string pin)
    {
        ApartmentId = apartmentId;
        Pin = pin;
    }
    #endregion
}

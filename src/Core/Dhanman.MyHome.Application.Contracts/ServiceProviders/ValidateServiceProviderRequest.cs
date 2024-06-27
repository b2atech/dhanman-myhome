namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;

public class ValidateServiceProviderRequest
{

    #region Properties    
  public Guid ApartmentId { get; set; }

    public char PinCode { get; set; }
    #endregion

    #region Constructor
    public ValidateServiceProviderRequest(Guid apartmentId, char pinCode)
    {
        ApartmentId = apartmentId;
        PinCode = pinCode;
    }
    #endregion
}

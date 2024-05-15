namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;
public class Address
{
    #region Properties    
    public Guid CountryId { get; set; }
    public Guid StateId { get; set; }
    public Guid CityId { get; set; }
    public string CityName { get; set; }
    public string AddressLine1 { get; set; }
    public string AddressLine2 { get; set; }
    public string ZipCode { get; set; }
    #endregion

    #region Constructor    
    public Address(Guid countryId, Guid stateId, Guid cityId, string addressLine1, string addressLine2, string zipCode)
    {
        CountryId = countryId;
        StateId = stateId;
        CityId = cityId;
        AddressLine1 = addressLine1;
        AddressLine2 = addressLine2;
        ZipCode = zipCode;
    }
    #endregion
}

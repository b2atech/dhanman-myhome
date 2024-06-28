namespace Dhanman.MyHome.Application.Contracts.DeliveryPersons;

public class CreateDeliveryPersonRequest
{
    #region Properties
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string MobileNumber { get; set; }

    #endregion

    #region Constructors
    public CreateDeliveryPersonRequest(string name, string companyName, string mobileNumber)
    {
        Name = name;
        CompanyName = companyName;
        MobileNumber = mobileNumber;
    }

    #endregion
}

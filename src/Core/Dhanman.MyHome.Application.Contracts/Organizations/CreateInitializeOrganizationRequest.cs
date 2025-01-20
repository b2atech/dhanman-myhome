namespace Dhanman.MyHome.Application.Contracts.Organizations;

public sealed class CreateInitializeOrganizationRequest
{

    #region Properties
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CompanyGuids { get; set; }
    public string CompanyNames { get; set; }
    public Guid UserId { get; set; }
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string PhoneNumber { get; set; }

    public string Email { get; set; }
    #endregion

    #region Constructor
    public CreateInitializeOrganizationRequest(Guid id, string name, string companyGuids, string companyNames, Guid userId, string userFirstName, string userLastName, string phoneNumber, string email)
    {
        Id = id;
        Name = name;
        CompanyGuids = companyGuids;
        CompanyNames = companyNames;
        UserId = userId;
        UserFirstName = userFirstName;
        UserLastName = userLastName;
        PhoneNumber = phoneNumber;
        Email = email;
    }
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts;

public sealed class CreateCompanyRequest
{
    #region Properties  
    public Guid CompanyId { get; set; }
    public Guid OrganizationId { get; set; }
    public string Name { get; set; }
    public bool IsApartment { get; set; }
    #endregion

    #region Constructors
    public CreateCompanyRequest() => Name = string.Empty;
    #endregion
}

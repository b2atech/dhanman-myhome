namespace Dhanman.MyHome.Application.Contracts.Organizations;

public class OrganizationResponse
{
    #region Properties
    public Guid Id { get; }
    public string Name { get; }
    #endregion

    #region Constructor
    public OrganizationResponse(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}

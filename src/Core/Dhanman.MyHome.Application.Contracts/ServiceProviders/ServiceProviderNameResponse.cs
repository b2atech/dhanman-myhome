namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;

public sealed class ServiceProviderNameResponse
{
    #region Properties 
    public int Id { get; }
    public string FirstName { get; }
    public string? LastName { get; }
    #endregion

    #region Constructor
    public ServiceProviderNameResponse(int id, string firstName, string? lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }  
    #endregion
}
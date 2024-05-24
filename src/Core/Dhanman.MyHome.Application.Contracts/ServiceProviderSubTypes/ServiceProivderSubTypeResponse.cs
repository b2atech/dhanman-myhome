namespace Dhanman.MyHome.Application.Contracts.ServiceProviderSubTypes;

public sealed class ServiceProivderSubTypeResponse
{
    #region Properties 
    public int Id { get; }
    public int ServiceProviderTypeId { get; }
    public string Name { get; }

    #endregion

    #region Constructor
    public ServiceProivderSubTypeResponse(int id, int serviceProviderTypeId, string name)
    {
        Id = id;
        ServiceProviderTypeId = serviceProviderTypeId;
        Name = name;
    }
    #endregion
}

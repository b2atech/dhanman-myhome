namespace Dhanman.MyHome.Application.Contracts.ServiceProviderTypes;

public sealed class ServiceProivderTypeResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }

    #endregion

    #region Constructor
    public ServiceProivderTypeResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}

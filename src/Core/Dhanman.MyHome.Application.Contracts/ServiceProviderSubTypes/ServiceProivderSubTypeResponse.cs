namespace Dhanman.MyHome.Application.Contracts.ServiceProviderSubTypes;

public sealed class ServiceProivderSubTypeResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }

    #endregion

    #region Constructor
    public ServiceProivderSubTypeResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}

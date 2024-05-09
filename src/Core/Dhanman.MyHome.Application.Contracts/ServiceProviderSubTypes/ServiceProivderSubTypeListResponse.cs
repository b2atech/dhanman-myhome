namespace Dhanman.MyHome.Application.Contracts.ServiceProviderSubTypes;

public sealed class ServiceProivderSubTypeListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<ServiceProivderSubTypeResponse> Items { get; }
    #endregion

    #region Constructor

    public ServiceProivderSubTypeListResponse(IReadOnlyCollection<ServiceProivderSubTypeResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}

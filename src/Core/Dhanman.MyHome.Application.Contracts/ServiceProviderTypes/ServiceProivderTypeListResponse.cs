namespace Dhanman.MyHome.Application.Contracts.ServiceProviderTypes;

public sealed class ServiceProivderTypeListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<ServiceProivderTypeResponse> Items { get; }
    #endregion

    #region Constructor

    public ServiceProivderTypeListResponse(IReadOnlyCollection<ServiceProivderTypeResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}

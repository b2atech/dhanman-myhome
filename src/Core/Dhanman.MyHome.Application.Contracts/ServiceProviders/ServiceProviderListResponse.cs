namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;

public sealed class ServiceProviderListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<ServiceProviderResponse> Items { get; }
    #endregion

    #region Constructor

    public ServiceProviderListResponse(IReadOnlyCollection<ServiceProviderResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
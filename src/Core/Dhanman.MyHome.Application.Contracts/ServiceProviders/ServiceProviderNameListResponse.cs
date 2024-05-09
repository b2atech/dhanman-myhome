using Dhanman.MyHome.Application.Contracts.Vehicles;

namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;


public sealed class ServiceProviderNameListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<ServiceProviderNameResponse> Items { get; }
    #endregion

    #region Constructor

    public ServiceProviderNameListResponse(IReadOnlyCollection<ServiceProviderNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
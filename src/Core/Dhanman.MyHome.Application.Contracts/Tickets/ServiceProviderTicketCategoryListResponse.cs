namespace Dhanman.MyHome.Application.Contracts.Tickets;

public sealed class ServiceProviderTicketCategoryListResponse
{
    #region Properties  
    public string Cursor { get; }
    public IReadOnlyCollection<ServiceProviderTicketCategoryResponse> Items { get; }
    #endregion

    #region Constructor
    public ServiceProviderTicketCategoryListResponse(IReadOnlyCollection<ServiceProviderTicketCategoryResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}

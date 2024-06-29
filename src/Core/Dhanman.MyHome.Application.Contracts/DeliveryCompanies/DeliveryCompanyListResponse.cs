namespace Dhanman.MyHome.Application.Contracts.DeliveryCompanies;

public sealed class DeliveryCompanyListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<DeliveryCompanyResponse> Items { get; }
    #endregion

    #region Constructor

    public DeliveryCompanyListResponse(IReadOnlyCollection<DeliveryCompanyResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
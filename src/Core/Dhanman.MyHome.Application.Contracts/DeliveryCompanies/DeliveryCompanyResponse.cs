namespace Dhanman.MyHome.Application.Contracts.DeliveryCompanies;

public sealed class DeliveryCompanyResponse
{
    #region Properties
    public int DeliveryCompanyId { get; }
    public string DeliveryCompanyName { get; }
    public int DeliveryCompanyCategoryId { get; }  
    public string DeliveryCompanyCategoryName { get; }
    public string DeliveryCompanyIcon { get; }
    #endregion

    #region Constructor
    public DeliveryCompanyResponse(int deliveryCompanyId, string deliveryCompanyName, int deliveryCompanyCategoryId, string deliveryCompanyCategoryName, string deliveryCompanyIcon)
    {
        DeliveryCompanyId = deliveryCompanyId;
        DeliveryCompanyName = deliveryCompanyName;
        DeliveryCompanyCategoryId = deliveryCompanyCategoryId;
        DeliveryCompanyCategoryName = deliveryCompanyCategoryName;
        DeliveryCompanyIcon = deliveryCompanyIcon;
    }

    #endregion
}
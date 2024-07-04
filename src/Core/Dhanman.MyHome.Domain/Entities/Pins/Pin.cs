using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Pins;

public class Pin : EntityInt
{
    #region Properties
    public int ServiceProviderId { get; set; }
    public int? VisitorId { get; set; }
    public int? DeliveryId { get; set; }
    public string PinCode { get; set; }
    public DateTime EffectiveStartDateTime { get; set; }
    public DateTime EffectiveEndDateTime { get; set; }
    #endregion

    #region Constructor
    public Pin( int serviceProviderId, int? visitorId, int? deliveryId, string pinCode, DateTime effectiveStartDateTime, DateTime effectiveEndDateTime)
    {
        ServiceProviderId = serviceProviderId;
        VisitorId = visitorId;
        DeliveryId = deliveryId;
        PinCode = pinCode;
        EffectiveStartDateTime = effectiveStartDateTime;
        EffectiveEndDateTime = effectiveEndDateTime;
    }
    #endregion
}

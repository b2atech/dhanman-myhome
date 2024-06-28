using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Deliveries;

public class Pin : EntityInt
{
    #region Properties
    public string PinCode { get; set; }
    public int? ServiceProviderId { get; set; }
    public int? VisitorId { get; set; }
    public int? DeliveryId { get; set; }
    public DateTime EffectiveStartDateTime { get; set; }
    public DateTime? EffectiveEndDateTime { get; set; }
    #endregion

    #region Contructors
    public Pin() { }
    public Pin(int id, int serviceProviderId, int visitorId, int deliveryId, string pinCode, DateTime effectiveStartDateTime, DateTime effectiveEndDateTime)
    {
        Id = id;
        ServiceProviderId = serviceProviderId;
        VisitorId = visitorId;
        DeliveryId = deliveryId;
        PinCode = pinCode;
        EffectiveStartDateTime = effectiveStartDateTime;
        EffectiveEndDateTime = effectiveEndDateTime;
    }
    #endregion

}

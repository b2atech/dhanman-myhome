using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Pins;

public class Pin : EntityInt, ISoftDeletableEntity
{
    #region Properties
    public int ServiceProviderId { get; set; }
    public int VisitorId { get; set; }
    public int DeliveryId { get; set; }
    public char PinCode { get; set; }
    public DateTime EffectiveStartDateTime { get; set; }
    public DateTime EffectiveEndDateTime { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Constructor
    public Pin( int serviceProviderId, int visitorId, int deliveryId, char pinCode, DateTime effectiveStartDateTime, DateTime effectiveEndDateTime)
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

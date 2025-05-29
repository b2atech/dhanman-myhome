using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;

public sealed class WaterTankerDelivery : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public Guid CompanyId { get; set; }
    public Guid VendorId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public TimeSpan DeliveryTime { get; set; }
    public int TankerCapacityLiters { get; set; }
    public int ActualReceivedLiters { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; protected set; }
    #endregion


    #region Constructor
    public WaterTankerDelivery() { }

    public WaterTankerDelivery(int id, Guid companyId, Guid vendorId, DateTime deliveryDate, TimeSpan deliveryTime, int tankerCapacityLiters, int actualReceivedLiters, Guid createdBy)
    {
        Id = id;
        CompanyId = companyId;
        VendorId = vendorId;
        DeliveryDate = deliveryDate;
        DeliveryTime = deliveryTime;
        TankerCapacityLiters = tankerCapacityLiters;
        ActualReceivedLiters = actualReceivedLiters;
        CreatedBy = createdBy;
        CreatedOnUtc = DateTime.UtcNow;
    }
    #endregion
}
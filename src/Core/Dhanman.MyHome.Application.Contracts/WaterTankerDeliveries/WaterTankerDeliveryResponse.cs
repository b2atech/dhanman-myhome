namespace Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;

public sealed class WaterTankerDeliveryResponse
{
    public int Id { get; set; }
    public DateTime DeliveryDate { get; set; }
    public TimeSpan DeliveryTime { get; set; }
    public Guid VendorId { get; set; }
    public string? VendorName { get; set; }
    public int TankerCapacityLiters { get; set; }
    public int ActualReceivedLiters { get; set; }
    public Guid CreatedBy { get; set; }
    public string? CreatedByName { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public string? ModifiedByName { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }

    public WaterTankerDeliveryResponse() { }

    public WaterTankerDeliveryResponse(int id, DateTime deliveryDate, TimeSpan deliveryTime, Guid vendorId, string? vendorName, int tankerCapacityLiters, int actualReceivedLiters, Guid createdBy, string? createdByName, DateTime createdOnUtc, Guid? modifiedBy, string? modifiedByName, DateTime? modifiedOnUtc)
    {
        Id = id;
        DeliveryDate = deliveryDate;
        DeliveryTime = deliveryTime;
        VendorId = vendorId;
        VendorName = vendorName;
        TankerCapacityLiters = tankerCapacityLiters;
        ActualReceivedLiters = actualReceivedLiters;
        CreatedBy = createdBy;
        CreatedByName = createdByName;
        CreatedOnUtc = createdOnUtc;
        ModifiedBy = modifiedBy;
        ModifiedByName = modifiedByName;
        ModifiedOnUtc = modifiedOnUtc;
    }
}

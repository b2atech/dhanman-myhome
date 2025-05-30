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
}

namespace Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;

public sealed class UpdateWaterTankerDeliveryRequest
{
    public int Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid VendorId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public TimeSpan DeliveryTime { get; set; }
    public int TankerCapacityLiters { get; set; }
    public int ActualReceivedLiters { get; set; }

}

using System.Text.Json.Serialization;

namespace Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;

public sealed class CreateWaterTankerDeliveryDto
{
    public Guid CompanyId { get; set; }

    public Guid VendorId { get; set; }

    public DateTime DeliveryDate { get; set; }

    public TimeSpan DeliveryTime { get; set; }

    public int TankerCapacityLiters { get; set; }

    public int ActualReceivedLiters { get; set; }

    public CreateWaterTankerDeliveryDto()
    {

    }

    public CreateWaterTankerDeliveryDto(Guid companyId, Guid vendorId, DateTime deliveryDate, TimeSpan deliveryTime, int tankerCapacityLiters, int actualReceivedLiters)
    {
        CompanyId = companyId;
        VendorId = vendorId;
        DeliveryDate = deliveryDate;
        DeliveryTime = deliveryTime;
        TankerCapacityLiters = tankerCapacityLiters;
        ActualReceivedLiters = actualReceivedLiters;
    }
}

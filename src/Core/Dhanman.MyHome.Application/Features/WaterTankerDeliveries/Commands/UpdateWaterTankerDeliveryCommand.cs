using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Commands;

public sealed class UpdateWaterTankerDeliveryCommand : ICommand<Result<EntityUpdatedResponse>>
{
    public int WaterTankerDeliveryId { get; set; }
    public Guid CompanyId { get; set; }
    public Guid VendorId { get; set; }
    public DateTime DeliveryDate { get; set; }
    public TimeSpan DeliveryTime { get; set; }
    public int TankerCapacityLiters { get; set; }
    public int ActualReceivedLiters { get; set; }

    public UpdateWaterTankerDeliveryCommand(int waterTankerDeliveryId, Guid companyId, Guid vendorId, DateTime deliveryDate, TimeSpan deliveryTime, int tankerCapacityLiters, int actualReceivedLiters)
    {
        WaterTankerDeliveryId = waterTankerDeliveryId;
        CompanyId = companyId;
        VendorId = vendorId;
        DeliveryDate = deliveryDate;
        DeliveryTime = deliveryTime;
        TankerCapacityLiters = tankerCapacityLiters;
        ActualReceivedLiters = actualReceivedLiters;
    }
}
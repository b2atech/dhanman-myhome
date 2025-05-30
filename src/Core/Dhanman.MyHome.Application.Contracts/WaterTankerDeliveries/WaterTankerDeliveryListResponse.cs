namespace Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;

public sealed class WaterTankerDeliveryListResponse
{
    public IReadOnlyCollection<WaterTankerDeliveryResponse> Items { get; }

    public WaterTankerDeliveryListResponse(IReadOnlyCollection<WaterTankerDeliveryResponse> items)
    {
        Items = items;
    }
}

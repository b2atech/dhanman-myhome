using Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IWaterTankerDeliveryRepository
{
    Task<WaterTankerDelivery> GetByIntIdAsync(int id);
    Task<IEnumerable<WaterTankerDelivery>> GetByCompanyAndVendorAsync(Guid companyId, Guid vendorId);
    void Insert(WaterTankerDelivery waterTankerDelivery);
    void Update(WaterTankerDelivery waterTankerDelivery);
    void Delete(WaterTankerDelivery waterTankerDelivery);
}

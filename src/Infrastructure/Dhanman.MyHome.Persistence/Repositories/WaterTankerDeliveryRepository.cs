using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

public sealed class WaterTankerDeliveryRepository : IWaterTankerDeliveryRepository
{
    private readonly IApplicationDbContext _dbContext;

    public WaterTankerDeliveryRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WaterTankerDelivery> GetByIntIdAsync(int id) => await _dbContext.GetBydIdIntAsync<WaterTankerDelivery>(id);

    public async Task<IEnumerable<WaterTankerDelivery>> GetByCompanyAndVendorAsync(Guid companyId, Guid vendorId)
    {
        return await _dbContext.SetInt<WaterTankerDelivery>()
            .Where(wtd => wtd.CompanyId == companyId && wtd.VendorId == vendorId)
            .ToListAsync();
    }

    public void Insert(WaterTankerDelivery waterTankerDelivery) => _dbContext.InsertInt(waterTankerDelivery);

    public void Update(WaterTankerDelivery waterTankerDelivery) => _dbContext.UpdateInt(waterTankerDelivery);

    public void Delete(WaterTankerDelivery waterTankerDelivery) => _dbContext.RemoveInt<WaterTankerDelivery>(waterTankerDelivery);
}
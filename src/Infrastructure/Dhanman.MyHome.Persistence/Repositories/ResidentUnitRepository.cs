using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

public class ResidentUnitRepository : IResidentUnitRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public ResidentUnitRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods
    public void Insert(ResidentUnit residentUnit) => _dbContext.InsertInt(residentUnit);

    public void Update(ResidentUnit residentUnit) => _dbContext.UpdateInt(residentUnit);

    public void Delete(ResidentUnit residentUnit) => _dbContext.RemoveInt(residentUnit);

    public Task<ResidentUnit?> GetBydIdIntAsync(int id) => _dbContext.GetBydIdIntAsync<ResidentUnit>(id);

    //public Task<int> GetLastResidentUnitIdAsync()
    //{
    //    throw new NotImplementedException();
    //}

    public DbSet<ResidentUnit> ResidentUnit => _dbContext.SetInt<ResidentUnit>();
    //public async Task<int> GetLastResidentIdAsync()
    //{
    //    return await _dbContext.SetInt<ResidentUnit>().
    //        IgnoreQueryFilters()
    //        .MaxAsync(b => b.Id);
    //}
    //public List<ResidentUnit> GetByResidentId(int residentId)
    //{
    //    var units = _dbContext.SetInt<ResidentUnit>().Where(u => u.ResidentId == residentId).ToList();
    //    return units;
    //}

    #endregion
}

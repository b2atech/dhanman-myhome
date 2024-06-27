using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;

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

    public Task<ResidentUnit?> GetByIdIntAsync(int id) => _dbContext.GetByIdIntAsync<ResidentUnit>(id);

    public Task<int> GetLastResidentUnitIdAsync()
    {
        throw new NotImplementedException();
    }

    public List<ResidentUnit> GetByResidentId(int residentId)
    {
        var units = _dbContext.SetInt<ResidentUnit>().Where(u => u.ResidentId == residentId).ToList();
        return units;
    }

    #endregion
}

using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Units;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class UnitRepository : IUnitRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public UnitRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<Unit> Unit => _dbContext.SetInt<Unit>();
    public async Task<Unit?> GetBydIdIntAsync(int id) => await _dbContext.GetBydIdIntAsync<Unit>(id);

    public void Insert(Unit unit) => _dbContext.InsertInt(unit);
    public void Delete(Unit unit) => _dbContext.RemoveInt(unit);
    public void Update(Unit unit) => _dbContext?.UpdateInt(unit);
    public int GetTotalRecordsCount() => Unit.Count();

    public async Task<bool> IsFlatValidAsync(string name)
    {
        bool isPresent =  await _dbContext.SetInt<Unit>()
                               .AnyAsync(u => u.Name == name);

        if (isPresent)
        {
            return false;
        }
        return true;
    }

    #endregion
}

using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Gates;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

public class GateRepository : IGateRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public GateRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods

    public Task<Gate?> GetByIntIdAsync(int id) => _dbContext.GetBydIdIntAsync<Gate>(id);

    //public async Task<int> GetLastGateIdAsync()
    //{
    //    return await _dbContext.SetInt<Gate>()
    //        .IgnoreQueryFilters()
    //        .OrderByDescending(g => g.Id)
    //        .Select(g => g.Id)
    //        .FirstOrDefaultAsync();
    //}

    public void Insert(Gate gate) => _dbContext.InsertInt(gate);

    public void Update(Gate gate) => _dbContext.UpdateInt(gate);

    public void Delete(Gate gate) => _dbContext.RemoveInt(gate);
    #endregion
}

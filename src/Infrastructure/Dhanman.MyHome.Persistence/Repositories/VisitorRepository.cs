using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

public class VisitorRepository : IVisitorRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public VisitorRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods

    public Task<Visitor?> GetByIntIdAsync(int id) => _dbContext.GetBydIdIntAsync<Visitor>(id);

    //public async Task<int> GetLastVisitorIdAsync()
    //{
    //    return await _dbContext.SetInt<Visitor>()
    //        .IgnoreQueryFilters()
    //        //.OrderByDescending(g => g.Id)
    //        //.Select(g => g.Id)
    //        //.FirstOrDefaultAsync();
    //        .MaxAsync(b => b.Id);
    //}

    public void Insert(Visitor visitor) => _dbContext.InsertInt(visitor);

    public void Update(Visitor visitor) => _dbContext.UpdateInt(visitor);

    public void Delete(Visitor visitor) => _dbContext.RemoveInt(visitor);

    #endregion


}

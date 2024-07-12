using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.VisitorUnitLogs;

namespace Dhanman.MyHome.Persistence.Repositories;

public class VisitorUnitLogRepository : IVisitorUnitLogRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public VisitorUnitLogRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods

    public Task<VisitorUnitLog?> GetByIntIdAsync(int id) => _dbContext.GetBydIdIntAsync<VisitorUnitLog>(id);

    public void Insert(VisitorUnitLog visitorUnitLog) => _dbContext.InsertInt(visitorUnitLog);

    public void Update(VisitorUnitLog visitorUnitLog) => _dbContext.UpdateInt(visitorUnitLog);

    public void Delete(VisitorUnitLog visitorUnitLog) => _dbContext.RemoveInt(visitorUnitLog);

    #endregion


}
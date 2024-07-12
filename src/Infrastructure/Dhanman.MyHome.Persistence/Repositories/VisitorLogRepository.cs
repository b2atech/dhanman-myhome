using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.VisitorLogs;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

public class VisitorLogRepository : IVisitorLogRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public VisitorLogRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods

    public Task<VisitorLog?> GetByIntIdAsync(int id) => _dbContext.GetBydIdIntAsync<VisitorLog>(id);
     
    public void Insert(VisitorLog visitorLog) => _dbContext.InsertInt(visitorLog);

    public void Update(VisitorLog visitorLog) => _dbContext.UpdateInt(visitorLog);

    public void Delete(VisitorLog visitorLog) => _dbContext.RemoveInt(visitorLog);

    public DbSet<VisitorLog> VisitorLog => _dbContext.SetInt<VisitorLog>();
    public int GetTotalRecordsCount() => VisitorLog.Count();

    #endregion


}
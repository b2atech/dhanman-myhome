using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ApprovedVisitors;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class ApprovedVisitorRepository : IApprovedVisitorRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public ApprovedVisitorRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods

    public async Task<ApprovedVisitor?> GetBydIdIntAsync(int id) => await _dbContext.GetBydIdIntAsync<ApprovedVisitor>(id);
    public void Insert(ApprovedVisitor approvedVisitor) => _dbContext.InsertInt(approvedVisitor);
    public void Delete(ApprovedVisitor approvedVisitor) => _dbContext.RemoveInt(approvedVisitor);
    public void Update(ApprovedVisitor approvedVisitor) => _dbContext?.UpdateInt(approvedVisitor);

    #endregion
}
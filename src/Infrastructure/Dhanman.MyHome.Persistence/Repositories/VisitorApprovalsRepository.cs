using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.VisitorApprovals;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class VisitorApprovalsRepository : IVisitorApprovalsRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public VisitorApprovalsRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods

    public async Task<VisitorApproval?> GetBydIdIntAsync(int id) => await _dbContext.GetBydIdIntAsync<VisitorApproval>(id);
    public void Insert(VisitorApproval visitorApproval) => _dbContext.InsertInt(visitorApproval);
    public void Delete(VisitorApproval visitorApproval) => _dbContext.RemoveInt(visitorApproval);
    public void Update(VisitorApproval visitorApproval) => _dbContext?.UpdateInt(visitorApproval);

    #endregion
}

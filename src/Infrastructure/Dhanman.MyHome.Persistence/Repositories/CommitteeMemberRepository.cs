using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.CommitteeMembers;

namespace Dhanman.MyHome.Persistence.Repositories;

internal class CommitteeMemberRepository : ICommitteeMemberRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CommitteeMemberRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<CommitteeMember?> GetByIdAsync(int id) => await _dbContext.GetBydIdIntAsync<CommitteeMember>(id);

    public void Insert(CommitteeMember company) => _dbContext.InsertInt(company);
    #endregion
}

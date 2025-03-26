using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.MemberAdditionalDetails;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class MemberAdditionalDetailRepository : IMemberAdditionalDetailRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public MemberAdditionalDetailRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<MemberAdditionalDetail> MemberAdditionalDetail => _dbContext.Set<MemberAdditionalDetail>();
    public async Task<MemberAdditionalDetail?> GetBydIdAsync(Guid id) => await _dbContext.GetBydIdAsync<MemberAdditionalDetail>(id);

    public void Insert(MemberAdditionalDetail memberAdditionalDetail) => _dbContext.Insert(memberAdditionalDetail);

    public void Delete(MemberAdditionalDetail memberAdditionalDetail) => _dbContext.Remove(memberAdditionalDetail);

    public void Update(MemberAdditionalDetail memberAdditionalDetail) => _dbContext?.Update(memberAdditionalDetail);
    //public int GetTotalRecordsCount() => MemberAdditionalDetail.Count();

    #endregion
}
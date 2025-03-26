using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.MemberRequests;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class MemberRequestRepository : IMemberRequestRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public MemberRequestRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<MemberRequest> MemberRequest => _dbContext.SetInt<MemberRequest>();
    public async Task<MemberRequest?> GetBydIdIntAsync(int id) => await _dbContext.GetBydIdIntAsync<MemberRequest>(id);

    public void Insert(MemberRequest memberRequest) => _dbContext.InsertInt(memberRequest);

    public void Delete(MemberRequest memberRequest) => _dbContext.RemoveInt(memberRequest);

    public void Update(MemberRequest memberRequest) => _dbContext?.UpdateInt(memberRequest);
    public int GetTotalRecordsCount() => MemberRequest.Count();

    #endregion
}
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.CommunityResidentRequests;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class CommunityResidentRequestRepository : ICommunityResidentRequestRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public CommunityResidentRequestRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<CommunityResidentRequest> CommunityResidentRequest => _dbContext.SetInt<CommunityResidentRequest>();
    public async Task<CommunityResidentRequest?> GetBydIdIntAsync(int id) => await _dbContext.GetBydIdIntAsync<CommunityResidentRequest>(id);

    public void Insert(CommunityResidentRequest communityResidentRequest) => _dbContext.InsertInt(communityResidentRequest);

    public void Delete(CommunityResidentRequest communityResidentRequest) => _dbContext.RemoveInt(communityResidentRequest);

    public void Update(CommunityResidentRequest communityResidentRequest) => _dbContext?.UpdateInt(communityResidentRequest);
    public int GetTotalRecordsCount() => CommunityResidentRequest.Count();

    #endregion
}
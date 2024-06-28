using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ResidentRequests;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class ResidentRequestRepository : IResidentRequestRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public ResidentRequestRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<ResidentRequest> ResidentRequest => _dbContext.SetInt<ResidentRequest>();
    public async Task<ResidentRequest?> GetByIdIntAsync(int id) => await _dbContext.GetByIdIntAsync<ResidentRequest>(id);

    public void Insert(ResidentRequest residentRequest) => _dbContext.InsertInt(residentRequest);

    public void Delete(ResidentRequest residentRequest) => _dbContext.RemoveInt(residentRequest);

    public void Update(ResidentRequest residentRequest) => _dbContext?.UpdateInt(residentRequest);
    public int GetTotalRecordsCount() => ResidentRequest.Count();

    #endregion
}
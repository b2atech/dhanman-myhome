using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Residents;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

internal sealed class ResidentRepository : IResidentRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public ResidentRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public DbSet<Resident> Resident => _dbContext.SetInt<Resident>();
    public async Task<Resident?> GetBydIdIntAsync(int id) => await _dbContext.GetBydIdIntAsync<Resident>(id);

    public void Insert(Resident resident) => _dbContext.InsertInt(resident);
    public void Delete(Resident resident) => _dbContext.RemoveInt(resident);
    public void Update(Resident resident) => _dbContext?.UpdateInt(resident);
    public int GetTotalRecordsCount() => Resident.Count();

    #endregion
}
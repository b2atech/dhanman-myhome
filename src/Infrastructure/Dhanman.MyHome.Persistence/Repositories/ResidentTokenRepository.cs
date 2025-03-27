using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.ResidentTokens;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Persistence.Repositories;

public class ResidentTokenRepository : IResidentTokenRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public ResidentTokenRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods
    public async Task<ResidentToken?> GetByResidentIdAsync(int id) => await _dbContext.Set<ResidentToken>().Where(x => x.ResidentId == id).FirstOrDefaultAsync();

    public void Insert(ResidentToken residentToken) => _dbContext.Insert(residentToken);
    public void Update(ResidentToken residentToken) => _dbContext.Update(residentToken);

    #endregion
}

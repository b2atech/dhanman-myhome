using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Companies;

namespace Dhanman.MyHome.Persistence.Repositories;

public class CompanyRepository : ICompanyRepository
{

    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CompanyRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Company?> GetByIdAsync(Guid id) => await _dbContext.GetBydIdAsync<Company>(id);

    public void Insert(Company company) => _dbContext.Insert(company);
    #endregion
}

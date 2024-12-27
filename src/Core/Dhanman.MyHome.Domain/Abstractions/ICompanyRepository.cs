using Dhanman.MyHome.Domain.Entities.Companies;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface ICompanyRepository
{
    #region Methods
    Task<Company?> GetByIdAsync(Guid id);
    void Insert(Company company);

    #endregion
}

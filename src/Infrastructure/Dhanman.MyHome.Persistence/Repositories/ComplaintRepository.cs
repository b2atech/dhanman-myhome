using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Complaints;

namespace Dhanman.MyHome.Persistence.Repositories;

public sealed class ComplaintRepository : IComplaintRepository
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Contructor
    public ComplaintRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    #endregion

    #region Methods

    public void Insert(Complaint complaint) => _dbContext.Insert(complaint);

    Task<Complaint?> IComplaintRepository.GetByIdAsync(Guid id) => _dbContext.GetBydIdAsync<Complaint>(id);

    #endregion

}

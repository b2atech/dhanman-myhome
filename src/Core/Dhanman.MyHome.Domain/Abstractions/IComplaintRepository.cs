using Dhanman.MyHome.Domain.Entities.Complaints;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IComplaintRepository
{
    #region Methods

    Task<Complaint?> GetByIdAsync(Guid id);

    void Insert(Complaint complaint);

    #endregion
}

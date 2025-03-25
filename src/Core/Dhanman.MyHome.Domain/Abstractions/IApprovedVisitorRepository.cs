using Dhanman.MyHome.Domain.Entities.ApprovedVisitors;

namespace Dhanman.MyHome.Domain.Abstractions;


public interface IApprovedVisitorRepository
{
    #region Methods
    Task<ApprovedVisitor> GetBydIdIntAsync(int id);

    void Insert(ApprovedVisitor approvedVisitor);

    void Update(ApprovedVisitor approvedVisitor);

    void Delete(ApprovedVisitor approvedVisitor);
    #endregion
}

using Dhanman.MyHome.Domain.Entities.Visitors;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IVisitorRepository
{
    #region Methods
    Task<Visitor?> GetByIntIdAsync(int id);
    void Insert(Visitor visitor);
    void Update(Visitor visitor);
    void Delete(Visitor visitor);
   // Task<int> GetLastVisitorIdAsync();
    #endregion
}

using Dhanman.MyHome.Domain.Entities.Gates;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IGateRepository
{
    #region Methods
    Task<Gate?> GetByIntIdAsync(int id);
    void Insert(Gate gate);
    void Update(Gate gate);
    void Delete(Gate gate);
    Task<int> GetLastGateIdAsync();

    #endregion
}

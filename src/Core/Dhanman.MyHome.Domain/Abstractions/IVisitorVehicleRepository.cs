using Dhanman.MyHome.Domain.Entities.VisitorVehicles;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IVisitorVehicleRepository
{
    #region Methods
    Task<VisitorVehicles?> GetByIntIdAsync(int id);
    void Insert(VisitorVehicles visitor);
    void Update(VisitorVehicles visitor);
    void Delete(VisitorVehicles visitor);
    Task<int> GetLastVisitorIdAsync();
    #endregion
}

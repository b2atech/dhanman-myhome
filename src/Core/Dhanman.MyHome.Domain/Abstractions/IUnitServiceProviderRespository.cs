using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.UnitServiceProviders;

namespace Dhanman.MyHome.Domain.Abstractions;


public interface IUnitServiceProviderRespository
{
    #region Methods
    Task<UnitServiceProvider?> GetByIdIntAsync(int id);

    void InsertInt(UnitServiceProvider unitServiceProvider);

    void DeleteInt(UnitServiceProvider unitServiceProvider);

    void UpdateInt(UnitServiceProvider unitServiceProvider);


    #endregion
}

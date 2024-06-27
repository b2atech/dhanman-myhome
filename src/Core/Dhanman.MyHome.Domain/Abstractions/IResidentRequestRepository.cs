using Dhanman.MyHome.Domain.Entities.ResidentRequests;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IResidentRequestRepository
{
    #region Methods
    Task<ResidentRequest?> GetByIdIntAsync(int id);

    void Insert(ResidentRequest residentRequest);

    void Delete(ResidentRequest residentRequest);

    void Update(ResidentRequest residentRequest);

    int GetTotalRecordsCount();

    #endregion

}
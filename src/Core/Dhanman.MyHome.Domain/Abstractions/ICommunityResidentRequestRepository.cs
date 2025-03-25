using Dhanman.MyHome.Domain.Entities.CommunityResidentRequests;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface ICommunityResidentRequestRepository
{
    #region Methods
    Task<CommunityResidentRequest?> GetBydIdIntAsync(int id);

    void Insert(CommunityResidentRequest communityResidentRequest);

    void Delete(CommunityResidentRequest communityResidentRequest);

    void Update(CommunityResidentRequest communityResidentRequest);

    int GetTotalRecordsCount();

    #endregion

}
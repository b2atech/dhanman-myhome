using Dhanman.MyHome.Domain.Entities.MemberRequests;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IMemberRequestRepository
{
    #region Methods
    Task<MemberRequest?> GetBydIdIntAsync(int id);

    void Insert(MemberRequest memberRequest);

    void Delete(MemberRequest memberRequest);

    void Update(MemberRequest memberRequest);

    int GetTotalRecordsCount();

    #endregion

}
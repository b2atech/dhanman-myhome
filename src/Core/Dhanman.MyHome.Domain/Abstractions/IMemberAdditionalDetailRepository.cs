using Dhanman.MyHome.Domain.Entities.MemberAdditionalDetails;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface IMemberAdditionalDetailRepository
{
    #region Methods
    Task<MemberAdditionalDetail?> GetBydIdAsync(Guid id);

    void Insert(MemberAdditionalDetail memberAdditionalDetail);

    void Delete(MemberAdditionalDetail memberAdditionalDetail);

    void Update(MemberAdditionalDetail memberAdditionalDetail);

   // int GetTotalRecordsCount();

    #endregion

}
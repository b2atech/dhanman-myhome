using Dhanman.MyHome.Domain.Entities.CommitteeMembers;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface ICommitteeMemberRepository
{
    #region Methods
    Task<CommitteeMember?> GetByIdAsync(int id);
    void Insert(CommitteeMember company);

    #endregion
}

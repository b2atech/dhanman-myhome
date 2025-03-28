namespace Dhanman.MyHome.Application.Contracts.MemberRequests;

public class UpdateMemberApproveStatusRequest
{
    #region Properties
    public int Id { get; set; }
    #endregion

    #region Constructors
    public UpdateMemberApproveStatusRequest(int id)
    {
        Id = id;
    }
    #endregion
}
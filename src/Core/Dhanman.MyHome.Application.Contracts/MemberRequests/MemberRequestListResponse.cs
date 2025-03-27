namespace Dhanman.MyHome.Application.Contracts.MemberRequests;

public sealed class MemberRequestListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<MemberRequestResponse> Items { get; }
    #endregion

    #region Constructor

    public MemberRequestListResponse(IReadOnlyCollection<MemberRequestResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
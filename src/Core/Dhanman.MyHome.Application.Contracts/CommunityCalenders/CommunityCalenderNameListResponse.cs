namespace Dhanman.MyHome.Application.Contracts.CommunityCalenders;

public sealed class CommunityCalenderNameListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<CommunityCalenderNameResponse> Items { get; }
    #endregion

    #region Constructor

    public CommunityCalenderNameListResponse(IReadOnlyCollection<CommunityCalenderNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}

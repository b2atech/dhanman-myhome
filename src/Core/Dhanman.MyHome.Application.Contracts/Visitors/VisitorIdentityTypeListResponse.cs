namespace Dhanman.MyHome.Application.Contracts.Visitors;

public sealed class VisitorIdentityTypeListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<VisitorIdentityTypeResponse> Items { get; }
    #endregion

    #region Constructor

    public VisitorIdentityTypeListResponse(IReadOnlyCollection<VisitorIdentityTypeResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
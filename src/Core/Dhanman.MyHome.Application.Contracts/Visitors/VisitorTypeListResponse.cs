namespace Dhanman.MyHome.Application.Contracts.Visitors;

public sealed class VisitorTypeListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<VisitorTypeResponse> Items { get; }
    #endregion

    #region Constructor

    public VisitorTypeListResponse(IReadOnlyCollection<VisitorTypeResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
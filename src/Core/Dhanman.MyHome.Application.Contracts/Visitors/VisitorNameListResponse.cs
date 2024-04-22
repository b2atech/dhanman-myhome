namespace Dhanman.MyHome.Application.Contracts.Visitors;
public sealed class VisitorNameListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<VisitorNameResponse> Items { get; }
    #endregion

    #region Constructor

    public VisitorNameListResponse(IReadOnlyCollection<VisitorNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
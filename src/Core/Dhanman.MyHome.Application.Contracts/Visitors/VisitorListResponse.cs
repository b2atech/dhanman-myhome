namespace Dhanman.MyHome.Application.Contracts.Visitors;

public sealed class VisitorListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<VisitorResponse> Items { get; }
    #endregion

    #region Constructor

    public VisitorListResponse(IReadOnlyCollection<VisitorResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
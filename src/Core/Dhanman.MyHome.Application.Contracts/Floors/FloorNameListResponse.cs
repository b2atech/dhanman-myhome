namespace Dhanman.MyHome.Application.Contracts.Floors;

public sealed class FloorNameListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<FloorNameResponse> Items { get; }
    #endregion

    #region Constructor

    public FloorNameListResponse(IReadOnlyCollection<FloorNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}

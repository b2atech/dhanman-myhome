namespace Dhanman.MyHome.Application.Contracts.Floors;

public sealed class FloorListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<FloorResponse> Items { get; }
    #endregion

    #region Constructor

    public FloorListResponse(IReadOnlyCollection<FloorResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}

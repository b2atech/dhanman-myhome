namespace Dhanman.MyHome.Application.Contracts.OccupancyTypes;

public sealed class OccupancyTypeListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<OccupancyTypeResponse> Items { get; }
    #endregion

    #region Constructor

    public OccupancyTypeListResponse(IReadOnlyCollection<OccupancyTypeResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
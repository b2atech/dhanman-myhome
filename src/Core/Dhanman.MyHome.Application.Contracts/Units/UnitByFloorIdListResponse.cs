namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitByFloorIdListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<UnitByFloorIdResponse> Items { get; }
    #endregion

    #region Constructor

    public UnitByFloorIdListResponse(IReadOnlyCollection<UnitByFloorIdResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<UnitResponse> Items { get; }
    #endregion

    #region Constructor

    public UnitListResponse(IReadOnlyCollection<UnitResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
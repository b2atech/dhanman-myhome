namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitNameListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<UnitNameResponse> Items { get; }
    #endregion

    #region Constructor

    public UnitNameListResponse(IReadOnlyCollection<UnitNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitDetailListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<UnitDetailResponse> Items { get; }
    #endregion

    #region Constructor

    public UnitDetailListResponse(IReadOnlyCollection<UnitDetailResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
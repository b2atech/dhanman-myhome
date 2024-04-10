namespace Dhanman.MyHome.Application.Contracts.Gates;

public sealed class GateListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<GateResponse> Items { get; }
    #endregion

    #region Constructor

    public GateListResponse(IReadOnlyCollection<GateResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}

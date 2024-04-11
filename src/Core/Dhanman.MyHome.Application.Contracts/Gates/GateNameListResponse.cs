namespace Dhanman.MyHome.Application.Contracts.Gates;

public sealed class GateNameListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<GateNameResponse> Items { get; }
    #endregion

    #region Constructor

    public GateNameListResponse(IReadOnlyCollection<GateNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}

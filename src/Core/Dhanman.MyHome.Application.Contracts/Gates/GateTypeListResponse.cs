namespace Dhanman.MyHome.Application.Contracts.Gates;

public sealed class GateTypeListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<GateTypeResponse> Items { get; }
    #endregion

    #region Constructor

    public GateTypeListResponse(IReadOnlyCollection<GateTypeResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}

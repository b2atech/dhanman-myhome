namespace Dhanman.MyHome.Application.Contracts.Visitors;

public sealed class VisitorsByUnitIdListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<VisitorsByUnitIdResponse> Items { get; }
    #endregion

    #region Constructor

    public VisitorsByUnitIdListResponse(IReadOnlyCollection<VisitorsByUnitIdResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
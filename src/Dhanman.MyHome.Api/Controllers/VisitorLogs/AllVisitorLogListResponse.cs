namespace Dhanman.MyHome.Application.Contracts.VisitorLogs;

public sealed class AllVisitorLogListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<AllVisitorLogResponse> Items { get; }
    #endregion

    #region Constructor

    public AllVisitorLogListResponse(IReadOnlyCollection<AllVisitorLogResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}

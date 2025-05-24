namespace Dhanman.MyHome.Application.Contracts.VisitorLogs;

public sealed class VisitorLogListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<VisitorLogResponse> Items { get; }
    #endregion

    #region Constructor

    public VisitorLogListResponse(IReadOnlyCollection<VisitorLogResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
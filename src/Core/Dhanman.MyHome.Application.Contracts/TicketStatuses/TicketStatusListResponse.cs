namespace Dhanman.MyHome.Application.Contracts.TicketStatuses;
 
public sealed class TicketStatusListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<TicketStatusResponse> Items { get; }
    #endregion

    #region Constructor

    public TicketStatusListResponse(IReadOnlyCollection<TicketStatusResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
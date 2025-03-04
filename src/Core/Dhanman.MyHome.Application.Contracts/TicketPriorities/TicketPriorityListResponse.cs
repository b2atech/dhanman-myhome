namespace Dhanman.MyHome.Application.Contracts.TicketPriorities;

public sealed class TicketPriorityListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<TicketPriorityResponse> Items { get; }
    #endregion

    #region Constructor

    public TicketPriorityListResponse(IReadOnlyCollection<TicketPriorityResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
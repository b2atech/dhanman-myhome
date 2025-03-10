namespace Dhanman.MyHome.Application.Contracts.Tickets;

public sealed class TicketListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<TicketResponse> Items { get; }
    #endregion

    #region Constructor

    public TicketListResponse(IReadOnlyCollection<TicketResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
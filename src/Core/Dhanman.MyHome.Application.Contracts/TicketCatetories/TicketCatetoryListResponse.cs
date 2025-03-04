namespace Dhanman.MyHome.Application.Contracts.TicketCatetories;

public sealed class TicketCatetoryListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<TicketCatetoryResponse> Items { get; }
    #endregion

    #region Constructor

    public TicketCatetoryListResponse(IReadOnlyCollection<TicketCatetoryResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
using Dhanman.MyHome.Application.Contracts.TicketCatetories;

namespace Dhanman.MyHome.Application.Contracts.TicketCategories;

public sealed class TicketCategoryListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<TicketCategoryResponse> Items { get; }
    #endregion

    #region Constructor

    public TicketCategoryListResponse(IReadOnlyCollection<TicketCategoryResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}
namespace Dhanman.MyHome.Application.Contracts.BookingFacilites;

public sealed class BookingFacilitesListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<BookingFacilitesResponse> Items { get; }
    #endregion

    #region Constructor

    public BookingFacilitesListResponse(IReadOnlyCollection<BookingFacilitesResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}


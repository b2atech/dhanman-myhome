namespace Dhanman.MyHome.Application.Contracts.Apartments;

public sealed class ApartmentNameListResponse
{
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<ApartmentNameResponse> Items { get; }
    #endregion

    #region Constructor

    public ApartmentNameListResponse(IReadOnlyCollection<ApartmentNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}

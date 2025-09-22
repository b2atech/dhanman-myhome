namespace Dhanman.MyHome.Application.Contracts.Units;

public class UnitNamesWithApartmentListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<UnitNamesWithApartmentResponse> Items { get; }
    #endregion

    #region Constructor

    public UnitNamesWithApartmentListResponse(IReadOnlyCollection<UnitNamesWithApartmentResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.Apartments;

    public sealed class ApartmentListResponse
    {
    #region Properties 
    public string Cursor { get; }
    public IReadOnlyCollection<ApartmentResponse> Items { get; }
    #endregion

    #region Constructor

    public ApartmentListResponse(IReadOnlyCollection<ApartmentResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion

}

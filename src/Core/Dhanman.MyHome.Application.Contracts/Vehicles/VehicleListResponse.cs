namespace Dhanman.MyHome.Application.Contracts.Vehicles;

public sealed class VehicleListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<VehicleResponse> Items { get; }
    #endregion

    #region Constructor

    public VehicleListResponse(IReadOnlyCollection<VehicleResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
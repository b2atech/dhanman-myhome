namespace Dhanman.MyHome.Application.Contracts.Vehicles;

public sealed class BasicVehicleInfoListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<BasicVehicleInfoResponse> Items { get; }
    #endregion

    #region Constructor

    public BasicVehicleInfoListResponse(IReadOnlyCollection<BasicVehicleInfoResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}

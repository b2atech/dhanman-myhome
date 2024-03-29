namespace Dhanman.MyHome.Application.Contracts.Vehicles;

public sealed class VehicleNameListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<VehicleNameResponse> Items { get; }
    #endregion

    #region Constructor

    public VehicleNameListResponse(IReadOnlyCollection<VehicleNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
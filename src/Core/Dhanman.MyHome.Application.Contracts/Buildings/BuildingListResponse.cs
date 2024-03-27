namespace Dhanman.MyHome.Application.Contracts.Buildings;

public sealed class BuildingListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<BuildingResponse> Items { get; }
    #endregion

    #region Constructor

    public BuildingListResponse(IReadOnlyCollection<BuildingResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
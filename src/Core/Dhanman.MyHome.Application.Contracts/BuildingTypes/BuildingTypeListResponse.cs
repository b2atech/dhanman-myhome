namespace Dhanman.MyHome.Application.Contracts.BuildingTypes;

public sealed class BuildingTypeListResponse
{
    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<BuildingTypeResponse> Items { get; }
    #endregion

    #region Constructor

    public BuildingTypeListResponse(IReadOnlyCollection<BuildingTypeResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}

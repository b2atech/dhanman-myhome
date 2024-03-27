namespace Dhanman.MyHome.Application.Contracts.Buildings;

public sealed class BuildingNameListResponse
{

    #region Properties 

    public string Cursor { get; }
    public IReadOnlyCollection<BuildingNameResponse> Items { get; }
    #endregion

    #region Constructor

    public BuildingNameListResponse(IReadOnlyCollection<BuildingNameResponse> items, string cursor = "")
    {
        Items = items;
        Cursor = cursor;
    }
    #endregion
}
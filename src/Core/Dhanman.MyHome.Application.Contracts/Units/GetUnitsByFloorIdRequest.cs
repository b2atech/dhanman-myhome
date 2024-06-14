namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class GetUnitsByFloorIdRequest
{
    public List<int> BuildingIds { get; set; }
    public List<int> FloorIds { get; set; }

    public GetUnitsByFloorIdRequest(List<int> buildingIds, List<int> floorIds)
    {
        BuildingIds = buildingIds;
        FloorIds = floorIds;
    }
}

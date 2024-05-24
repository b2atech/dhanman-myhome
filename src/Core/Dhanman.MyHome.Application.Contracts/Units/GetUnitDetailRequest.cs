namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class GetUnitDetailRequest
{
    public List<int> BuildingIds { get; set; }
    public List<int> OccupancyIds { get; set; }

    public GetUnitDetailRequest(List<int> buildingIds, List<int> occupancyIds)
    {
       BuildingIds = buildingIds;
       OccupancyIds = occupancyIds;
    }
 
}


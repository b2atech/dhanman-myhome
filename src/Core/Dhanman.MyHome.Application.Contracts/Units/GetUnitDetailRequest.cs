namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class GetUnitDetailRequest
{
    public List<int> BuidingIds { get; set; }
    public List<int> OccupancyIds { get; set; }

    public GetUnitDetailRequest(List<int> buidingIds, List<int> occupancyIds)
    {
       BuidingIds = buidingIds;
       OccupancyIds = occupancyIds;
    }
 
}

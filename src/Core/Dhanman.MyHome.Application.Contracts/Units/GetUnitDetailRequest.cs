namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class GetUnitDetailRequest
{
    public Guid ApartmentId { get; set; }
    public List<int> BuildingIds { get; set; }
    public List<int> OccupancyIds { get; set; }

    public GetUnitDetailRequest(Guid apartmentId, List<int> buildingIds, List<int> occupancyIds)
    {
        ApartmentId = apartmentId;
        BuildingIds = buildingIds;
        OccupancyIds = occupancyIds;
    }
 
}


namespace Dhanman.MyHome.Application.Contracts.UnitServiceProviders;

public sealed class GetAssignUnitsResquest
{
    public List<int> BuildingIds { get; set; }

    public GetAssignUnitsResquest(List<int> buildingIds)
    {
        BuildingIds = buildingIds;
    }
}

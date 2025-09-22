namespace Dhanman.MyHome.Application.Contracts.Units;

public class UnitNamesWithApartmentResponse
{
    public int Id { get; set; }

    public int UnitId { get; set; }

    public string UnitName { get; set; } = string.Empty;

    public string BuildingName { get; set; } = string.Empty;

    public string FloorName { get; set; } = string.Empty;

    public UnitNamesWithApartmentResponse(int id, int unitId, string unitName, string buildingName, string floorName)
    {
        Id = id;
        UnitId = unitId;
        UnitName = unitName;
        BuildingName = buildingName;
        FloorName = floorName;
    }

}

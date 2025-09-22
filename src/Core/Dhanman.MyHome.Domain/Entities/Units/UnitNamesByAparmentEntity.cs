using B2aTech.CrossCuttingConcern.Core.Primitives;
using System.Text.Json.Serialization;

namespace Dhanman.MyHome.Domain.Entities.Units;

public class UnitNamesByAparmentEntity :EntityInt
{
    [JsonPropertyName("unit_id")]
    public int UnitId { get; set; }

    [JsonPropertyName("unit_name")]
    public string UnitName { get; set; } = string.Empty;

    [JsonPropertyName("building_name")]
    public string BuildingName { get; set; } = string.Empty;

    [JsonPropertyName("floor_name")]
    public string FloorName { get; set; } = string.Empty;

    public UnitNamesByAparmentEntity(int id, int unitId, string unitName, string buildingName, string floorName)
    {
        Id = id;
        UnitId = unitId;
        UnitName = unitName;
        BuildingName = buildingName;
        FloorName = floorName;
    }
}

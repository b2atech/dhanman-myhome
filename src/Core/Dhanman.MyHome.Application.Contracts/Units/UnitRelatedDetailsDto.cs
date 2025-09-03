using System.Text.Json.Serialization;

namespace Dhanman.MyHome.Application.Contracts.Units;

public class UnitRelatedDetailsDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("unit_type_id")]
    public int UnitTypeId { get; set; }

    [JsonPropertyName("phone_extension")]
    public int PhoneExtension { get; set; }

    [JsonPropertyName("area")]
    public decimal Area { get; set; }

    [JsonPropertyName("bhk_type")]
    public int BhkType { get; set; }

    [JsonPropertyName("occupant_type_id")]
    public int OccupantTypeId { get; set; }

    [JsonPropertyName("occupancy_type_id")]
    public int OccupancyTypeId { get; set; }

    [JsonPropertyName("residents")]
    public List<ResidentDto> Residents { get; set; } = new List<ResidentDto>();

    [JsonPropertyName("vehicles")]
    public List<VehicleDto> Vehicles { get; set; } = new List<VehicleDto>();
}

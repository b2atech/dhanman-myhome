using System.Text.Json.Serialization;

namespace Dhanman.MyHome.Application.Contracts.Units;

public class VehicleDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("vehicle_number")]
    public string VehicleNumber { get; set; }

    [JsonPropertyName("vehicle_type")]
    public string VehicleType { get; set; }
}

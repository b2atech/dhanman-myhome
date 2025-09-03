using System.Text.Json.Serialization;

namespace Dhanman.MyHome.Application.Contracts.Units;

public class ResidentDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("contact_number")]
    public string ContactNumber { get; set; }

    [JsonPropertyName("resident_type")]
    public string ResidentType { get; set; }
}

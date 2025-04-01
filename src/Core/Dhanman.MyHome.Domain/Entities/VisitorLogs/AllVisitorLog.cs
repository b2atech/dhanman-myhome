using B2aTech.CrossCuttingConcern.Core.Primitives;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Dhanman.MyHome.Domain.Entities.VisitorLogs;

[Keyless]
public class AllVisitorLog : EntityInt
{
    #region Properties

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("visitor_id")]
    public int VisitorId { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("unit_id")]
    public int UnitId { get; set; }

    [JsonPropertyName("unit_name")]
    public string? UnitName { get; set; }

    [JsonPropertyName("latest_entry_time")]
    public DateTime LatestEntryTime { get; set; }

    [JsonPropertyName("latest_exit_time")]
    public DateTime? LatestExitTime { get; set; }

    [JsonPropertyName("visiting_from")]
    public string? VisitingFrom { get; set; }

    [JsonPropertyName("contact_number")]
    public string? ContactNumber { get; set; }

    [JsonPropertyName("visitor_type_id")]
    public int VisitorTypeId { get; set; }

    [JsonPropertyName("visitor_type_name")]
    public string VisitorTypeName { get; set; }

    #endregion

}

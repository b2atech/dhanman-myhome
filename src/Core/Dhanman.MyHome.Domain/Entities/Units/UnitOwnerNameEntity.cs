using B2aTech.CrossCuttingConcern.Core.Primitives;
using System.Text.Json.Serialization;

namespace Dhanman.MyHome.Domain.Entities.Units;

public class UnitOwnerNameEntity : EntityInt
{
   
    [JsonPropertyName("customer_id")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("unit_id")]
    public int UnitId { get; set; }

    [JsonPropertyName("unit_name")]
    public string UnitName { get; set; } = string.Empty;

    [JsonPropertyName("owner_first_name")]
    public string OwnerFirstName { get; set; } = string.Empty;

    [JsonPropertyName("owner_last_name")]
    public string OwnerLastName { get; set; } = string.Empty;


    public UnitOwnerNameEntity(int id, Guid customerId, int unitId, string unitName, string ownerFirstName, string ownerLastName)
    {
        Id = id;
        CustomerId = customerId;
        UnitId = unitId;
        UnitName = unitName;
        OwnerFirstName = ownerFirstName;
        OwnerLastName = ownerLastName;
    }
}

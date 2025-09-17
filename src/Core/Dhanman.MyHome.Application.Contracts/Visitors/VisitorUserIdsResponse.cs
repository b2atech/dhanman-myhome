using System.ComponentModel.DataAnnotations.Schema;

namespace Dhanman.MyHome.Application.Contracts.Visitors;

public class VisitorUserIdsResponse
{
    [Column("id")]
    public int Id { get; set; }

    [Column("user_ids")]
    public Guid[] UserIds { get; set; } = Array.Empty<Guid>();

    [Column("visitor_id")]
    public int VisitorId { get; set; }

    [Column("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [Column("last_name")]
    public string? LastName { get; set; }

    [Column("fcm_tokens")]
    public string[] FcmTokens { get; set; } = Array.Empty<string>();

    [Column("device_ids")]
    public string[] DeviceIds { get; set; } = Array.Empty<string>();

    public VisitorUserIdsResponse(int id, Guid[] userIds, int visitorId, string firstName, string? lastName, string[] fcmTokens, string[] deviceIds)
    {
        Id = id;
        UserIds = userIds;
        VisitorId = visitorId;
        FirstName = firstName;
        LastName = lastName;
        FcmTokens = fcmTokens;
        DeviceIds = deviceIds;
    }
}

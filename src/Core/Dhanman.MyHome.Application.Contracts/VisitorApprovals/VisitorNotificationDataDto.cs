using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Application.Contracts.VisitorApprovals;

public class VisitorNotificationDataDto : EntityInt
{
    public int Id { get; set; }
    public List<Guid> UserIds { get; set; } = new();
    public int VisitorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> FcmTokens { get; set; } = new();
    public List<string> DeviceIds { get; set; } = new();
}
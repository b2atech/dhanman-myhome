using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Visitors;

public class VisitorUserIdsDto : EntityInt
{
    public int Id { get; set; } 
    public Guid[] UserIds { get; set; } = Array.Empty<Guid>();
    public int VisitorId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
}

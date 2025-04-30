using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Visitors;

public class VisitorDbDto : Entity
{
    public int Id { get; set; }
    public string FirstName { get; set; } 
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? VisitingFrom { get; set; }
    public string ContactNumber { get; set; }
    public int VisitorTypeId { get; set; }
    public string VisitorTypeName { get; set; }
    public string? VehicleNumber { get; set; }
    public int? IdentityTypeId { get; set; }
    public string? IdentityNumber { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
}

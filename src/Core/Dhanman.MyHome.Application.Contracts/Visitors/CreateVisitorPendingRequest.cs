namespace Dhanman.MyHome.Application.Contracts.Visitors;

public class CreateVisitorPendingRequest
{
    #region Properties
    public Guid ApartmentId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? VisitingFrom { get; set; }
    public string ContactNumber { get; set; }
    public int VisitorTypeId { get; set; }
    public int CurrentStatusId { get; set; }
    public string? VehicleNumber { get; set; }
    public int? IdentityTypeId { get; set; }
    public string? IdentityNumber { get; set; }
    public Guid CreatedBy { get; set; }
    #endregion
}

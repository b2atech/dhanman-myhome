namespace Dhanman.MyHome.Application.Contracts.Visitors;

public class CreateVisitorRequest
{
    #region Properties
    public Guid ApartmentId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string VisitingFrom { get; set; }
    public string ContactNumber { get; set; }
    public int VisitorTypeId { get; set; }
    public string? VehicleNumber { get; set; }
    public int IdentityTypeId { get; set; }
    public string IdentityNumber { get; set; }
    public DateTime EntryTime { get; set; }
    public DateTime? ExitTime { get; set; }
    #endregion

    #region Constructors
    public CreateVisitorRequest()
    {
    }
    #endregion

}

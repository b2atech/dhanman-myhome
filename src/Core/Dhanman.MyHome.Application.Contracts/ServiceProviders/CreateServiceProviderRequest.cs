namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;

public class CreateServiceProviderRequest
{
    #region Properties    
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string VisitingFrom { get; set; }
    public string ContactNumber { get; set; }
    public Address PermanentAddress { get; set; }
    public Address PresentAddress { get; set; }
    public int ServiceProviderTypeId { get; set; }
    public int ServiceProviderSubTypeId { get; set; }
    public string? VehicleNumber { get; set; }
    public int IdentityTypeId { get; set; }
    public string IdentityNumber { get; set; }
    public DateTime ValidityDate { get; set; }
    public bool PoliceVerificationStatus { get; set; }
    public bool IsHireable { get; set; }
    public bool IsVisible { get; set; }
    public bool IsFrequentVisitor { get; set; }
    public Guid ApartmentId { get; set; }
    public string PinCode { get; set; }
    public DateTime CreatedOnUtc { get; }
    public Guid CreatedBy { get; set; }
    #endregion

    #region Constructors
    public CreateServiceProviderRequest() => FirstName = string.Empty;
    #endregion
}

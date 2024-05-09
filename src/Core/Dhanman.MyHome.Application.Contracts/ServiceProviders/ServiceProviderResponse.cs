namespace Dhanman.MyHome.Application.Contracts.ServiceProviders;
 
public sealed class ServiceProviderResponse
{
    #region Properties 
    public int Id { get; }
    public string FirstName { get; }
    public string? LastName { get; }
    public string? Email { get; }
    public string VisitingFrom { get; }
    public string ContactNumber { get; }
    public Guid PrermanentAddressId { get; }
    public Guid PresentAddressId { get; }    
    public int ServiceProviderTypeId { get; }
    public string ServiceProviderType { get; }
    public int ServiceProviderSubTypeId { get; }
    public string ServiceProviderSubType { get; }
    public string? VehicleNumber { get; }
    public int IdentityTypeId { get; }
    public string IdentityNumber { get; }    
    public Guid CreatedBy { get; }
    public DateTime CreatedOnUtc { get; }
    public Guid? ModifiedBy { get; }
    public DateTime? ModifiedOnUtc { get; }

    #endregion

    #region Constructor
    public ServiceProviderResponse(int id, string firstName, string? lastName, string? email, string visitingFrom, string contactNumber, Guid prermanentAddressId, Guid presentAddressId, int serviceProviderTypeId, string serviceProviderType, int serviceProviderSubTypeId, string serviceProviderSubType, string? vehicleNumber, int identityTypeId, string identityNumber, Guid createdBy, DateTime createdOnUtc, Guid? modifiedBy, DateTime? modifiedOnUtc)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        VisitingFrom = visitingFrom;
        ContactNumber = contactNumber;
        PrermanentAddressId = prermanentAddressId;
        PresentAddressId = presentAddressId;
        ServiceProviderTypeId = serviceProviderTypeId;
        ServiceProviderType = serviceProviderType;
        ServiceProviderSubTypeId = serviceProviderSubTypeId;
        ServiceProviderSubType = serviceProviderSubType;
        VehicleNumber = vehicleNumber;
        IdentityTypeId = identityTypeId;
        IdentityNumber = identityNumber;
        CreatedBy = createdBy;
        CreatedOnUtc = createdOnUtc;
        ModifiedBy = modifiedBy;
        ModifiedOnUtc = modifiedOnUtc;
    }
    #endregion


}
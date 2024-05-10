using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.ServiceProviders;

public class ServiceProvider : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string VisitingFrom { get; set; }
    public string ContactNumber { get; set; }
    public Guid PermanentAddressId { get; set; }
    public Guid PresentAddressId { get; set; }
    //Daily Help
    //Tutors
    //Handyman
    //Vendors
    //Medical Help
    //Transport
    //Society Security
    //Society Administration Staff
    //Society Maintenance Staff
    //Full Time Helps
    public int ServiceProviderTypeId { get; set; }
    public int ServiceProviderSubTypeId { get; set; }
    public string? VehicleNumber { get; set; }
    public int IdentityTypeId { get; set; }
    public string IdentityNumber { get; set; }
    public byte[] IdentityImage { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool IsDeleted { get; }

    public Guid CreatedBy { get; }

    public Guid? ModifiedBy { get; }
    #endregion

    #region Constructor
    public ServiceProvider(int id, string firstName, string? lastName, string? email, string visitingFrom, string contactNumber, Guid permanentAddressId, Guid presentAddressId, int serviceProviderTypeId, int serviceProviderSubTypeId, string? vehicleNumber, int identityTypeId, string identityNumber,  Guid createdBy)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        VisitingFrom = visitingFrom;
        ContactNumber = contactNumber;
        PermanentAddressId = permanentAddressId;
        PresentAddressId = presentAddressId;
        ServiceProviderTypeId = serviceProviderTypeId;
        ServiceProviderSubTypeId = serviceProviderSubTypeId;
        VehicleNumber = vehicleNumber;
        IdentityTypeId = identityTypeId;
        IdentityNumber = identityNumber;      
        CreatedBy = createdBy;
        CreatedOnUtc = DateTime.UtcNow;
    }    
    #endregion
}

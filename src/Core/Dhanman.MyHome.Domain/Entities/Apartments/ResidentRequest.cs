using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Apartments;

public class ResidentRequest : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int UnitId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
    public string? PermanentAddressId { get; set; }

    //Co-Owner, Multi Tenant, Owner, Owner Family , Tenant, Tenant Family 
    public string ResidentTypeId { get; set; }

    //Vacant , Residing , Let out to multiple tenants , Let out to one tenant, 
    public string OccupancyStatusId { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool IsDeleted { get; }

    public Guid CreatedBy { get; }

    public Guid? ModifiedBy { get;  }
    #endregion

    #region Constructor
    public ResidentRequest(int id, int unitId, string firstName, string lastName, string email, string contactNumber, string? permanentAddressId, string residentTypeId, string occupancyStatusId, Guid createdBy)
    {
        Id = id; 
        UnitId = unitId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactNumber = contactNumber;
        PermanentAddressId = permanentAddressId;
        ResidentTypeId = residentTypeId;
        OccupancyStatusId = occupancyStatusId;
        CreatedBy = createdBy;
        CreatedOnUtc = DateTime.UtcNow;
    }
    #endregion
}

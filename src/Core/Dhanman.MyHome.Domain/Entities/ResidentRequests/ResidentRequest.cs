using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.ResidentRequests;

public class ResidentRequest : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public Guid ApartmentId { get; set; }
    public int BuildingId { get; set; }
    public int FloorId { get; set; }
    public int UnitId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
    public Guid PermanentAddressId { get; set; }
    public int RequestStatusId { get; set; }
    //Co-Owner, Multi Tenant, Owner, Owner Family , Tenant, Tenant Family 
    public int ResidentTypeId { get; set; }

    //Vacant , Residing , Let out to multiple tenants , Let out to one tenant, 
    public int OccupancyStatusId { get; set; }

    public Guid MemberAdditionalDetailsId { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool IsDeleted { get; }

    public Guid CreatedBy { get; protected set; }

    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructor
    public ResidentRequest(int id, int unitId, string firstName, string lastName, string email, string contactNumber, Guid? permanentAddressId, int requestStatusId, int residentTypeId, int occupancyStatusId)
    {
        Id = id;       
        UnitId = unitId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactNumber = contactNumber;
        PermanentAddressId = permanentAddressId ?? Guid.Empty;
        RequestStatusId = requestStatusId;
        ResidentTypeId = residentTypeId;
        OccupancyStatusId = occupancyStatusId;       
        CreatedOnUtc = DateTime.UtcNow;
    } 
    public ResidentRequest(int id, Guid apartmentId, string firstName, string lastName, string email, string contactNumber, Guid permanentAddressId, int requestStatusId, int residentTypeId, int occupancyStatusId, Guid memberAdditionalDetailsId)
    {
        Id = id;
        ApartmentId = apartmentId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactNumber = contactNumber;
        PermanentAddressId = permanentAddressId;
        RequestStatusId = requestStatusId;
        ResidentTypeId = residentTypeId;
        OccupancyStatusId = occupancyStatusId;
        MemberAdditionalDetailsId = memberAdditionalDetailsId;
    }

    public ResidentRequest(int id, string firstName, string lastName, string email, string contactNumber, int requestStatusId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactNumber = contactNumber;      
        RequestStatusId = requestStatusId;       
    }
    #endregion
}

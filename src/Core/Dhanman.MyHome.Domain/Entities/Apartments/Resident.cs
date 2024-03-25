using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Apartments;

public class Resident : EntityInt, IAuditableEntity, ISoftDeletableEntity
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
    public string? PermanentAddressId { get; set; }
    public int RequestStatusId { get; set; }
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

    public Guid CreatedBy { get; protected set; }

    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructor
    public Resident(int id, Guid apartmentId, int buildingId, int floorId, int unitId, string firstName, string lastName, string email, string contactNumber, string? permanentAddressId, int requestStatusId, string residentTypeId, string occupancyStatusId, Guid createdBy)
    {
        Id = id; 
        ApartmentId = apartmentId;
        BuildingId = buildingId;
        FloorId = floorId;
        UnitId = unitId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        ContactNumber = contactNumber;
        PermanentAddressId = permanentAddressId;
        RequestStatusId = requestStatusId;
        ResidentTypeId = residentTypeId;
        OccupancyStatusId = occupancyStatusId;
        CreatedBy = createdBy;
    }
    #endregion
}

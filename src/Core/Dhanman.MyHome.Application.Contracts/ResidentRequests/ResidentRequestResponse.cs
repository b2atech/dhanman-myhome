namespace Dhanman.MyHome.Application.Contracts.ResidentRequests;
 
public sealed class ResidentRequestResponse
{
    #region Properties 
    public int Id { get; }
    public Guid ApartmentId { get; }
    public string ApartmentType { get; }
    public int BuildingId { get; }
    public string BuildingType { get; }
    public int FloorId { get; }
    public string Floor { get; }
    public int UnitId { get; }
    public string Unit { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string RequestedByName { get; }
    public string Email { get; }
    public string ContactNumber { get; }
    public Guid? PermanentAddressId { get; }
    public int RequestStatusId { get; }
    public string RequestStatus { get; }
    public int ResidentTypeId { get; }
    public string ResidentType { get; }
    public int OccupancyStatusId { get; } // unit status
    public string OccupancyStatus { get; }
    public DateTime CreatedOnUtc { get; }
    public Guid CreatedBy { get; }     
    public DateTime? ModifiedOnUtc { get; }     
    public Guid? ModifiedBy { get; }

    #endregion

    #region Constructor
    public ResidentRequestResponse(int id, Guid apartmentId, string apartmentType, int buildingId, string buildingType, int floorId, string floor, int unitId, string unit, string firstName, string lastName, string email, string contactNumber, Guid? permanentAddressId, int requestStatusId, string requestStatus, int residentTypeId, string residentType, int occupancyStatusId, string occupancyStatus, DateTime createdOnUtc, Guid createdBy, DateTime? modifiedOnUtc, Guid? modifiedBy)
    {
        Id = id;
        ApartmentId = apartmentId;
        ApartmentType = apartmentType;
        BuildingId = buildingId;
        BuildingType = buildingType;
        FloorId = floorId;
        Floor = floor;
        UnitId = unitId;
        Unit = unit;
        FirstName = firstName;
        LastName = lastName;
        RequestedByName = $"{firstName} {lastName}";        
        Email = email;
        ContactNumber = contactNumber;
        PermanentAddressId = permanentAddressId;
        RequestStatusId = requestStatusId;
        RequestStatus = requestStatus;
        ResidentTypeId = residentTypeId;
        ResidentType = residentType;
        OccupancyStatusId = occupancyStatusId;
        OccupancyStatus = occupancyStatus;
        CreatedOnUtc = createdOnUtc;
        CreatedBy = createdBy;
        ModifiedOnUtc = modifiedOnUtc;
        ModifiedBy = modifiedBy;
    }   

    #endregion
}
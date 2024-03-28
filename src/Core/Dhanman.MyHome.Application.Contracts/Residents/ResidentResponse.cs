namespace Dhanman.MyHome.Application.Contracts.Residents;

public sealed class ResidentResponse
{
    #region Properties 
    public int Id { get; }   
    public int UnitId { get; }
    public string Unit { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string ResidentName { get; }
    public string Email { get; }
    public string ContactNumber { get; }  
    public int ResidentTypeId { get; }
    public string ResidentType { get; }
    public int OccupancyStatusId { get; }  // unit status
    public string OccupancyStatus { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }

    #endregion

    #region Constructor
    public ResidentResponse(int id, int unitId, string unit, string firstName, string lastName, string email, string contactNumber,  int residentTypeId, string residentType, int occupancyStatusId, string occupancyStatus, DateTime createdOnUtc, DateTime? modifiedOnUtc, Guid createdBy, Guid? modifiedBy)
    {
        Id = id;
        UnitId = unitId;
        Unit = unit;
        FirstName = firstName;
        LastName = lastName;
        ResidentName = $"{firstName} {lastName}";
        Email = email;
        ContactNumber = contactNumber;  
        ResidentTypeId = residentTypeId;
        ResidentType = residentType;
        OccupancyStatusId = occupancyStatusId;
        OccupancyStatus = occupancyStatus;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
    }

    #endregion
}
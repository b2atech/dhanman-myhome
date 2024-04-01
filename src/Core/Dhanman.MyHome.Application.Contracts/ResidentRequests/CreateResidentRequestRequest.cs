namespace Dhanman.MyHome.Application.Contracts.ResidentRequests;

public class CreateResidentRequestRequest
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
    public int ResidentTypeId { get; set; }
    public int OccupancyStatusId { get; set; }
    public DateTime CreatedOnUtc { get; }
    public Guid CreatedBy { get; set; }

    #endregion

    #region Constructors
    public CreateResidentRequestRequest() => ApartmentId = Guid.Empty;
    #endregion
}
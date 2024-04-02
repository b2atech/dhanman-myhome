namespace Dhanman.MyHome.Application.Contracts.Residents;

public class CreateResidentRequest
{
    #region Properties    
    public int UnitId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ContactNumber { get; set; }
    public Guid PermanentAddressId { get; set; }
    public int ResidentTypeId { get; set; }
    public int OccupancyStatusId { get; set; }
    public DateTime CreatedOnUtc { get; }
    public Guid CreatedBy { get; set; }

    #endregion

    #region Constructors
    public CreateResidentRequest() => FirstName = string.Empty;
    #endregion
}
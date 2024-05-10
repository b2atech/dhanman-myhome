namespace Dhanman.MyHome.Application.Contracts.Residents;

public class CreateEventRequest
{
    #region Properties    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsFullDay { get; set; }
    public string BackgroundColor { get; set; }
    public string TextColor { get; set; }
    public int UnitId { get; set; }
    public DateTime ReservationDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Pourpose { get; set; }
    public int StatusId { get; set; }
    public int BookingFacilitiesId { get; set; }

    #endregion

    #region Constructors
    public CreateEventRequest() => Name = string.Empty;
    #endregion
}
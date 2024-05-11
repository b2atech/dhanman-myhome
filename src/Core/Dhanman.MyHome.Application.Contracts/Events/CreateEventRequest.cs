namespace Dhanman.MyHome.Application.Contracts.Residents;

public class CreateEventRequest
{
    #region Properties    
    public string Title { get; set; }
    public string Description { get; set; }
    public bool AllDay { get; set; }
    public string BackgroundColor { get; set; }
    public string TextColor { get; set; }
    public int ReserverdByUnitId { get; set; }
    public DateTime ReservationDate { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
    public string Pourpose { get; set; }
    public int StatusId { get; set; }
    public int BookingFacilitiesId { get; set; }

    #endregion

    #region Constructors
    public CreateEventRequest() => Title = string.Empty;
    #endregion
}
namespace Dhanman.MyHome.Application.Contracts.Events;

public class EventResponse
{
    public Guid Id { get; }
    public string Title { get; }
    public string Description { get; }
    public bool AllDay { get; }
    public string Color { get; }
    public string TextColor { get; }
    public int ReservationByUnitId { get; }
    public DateTime ReservationDate { get; }
    public string Start { get; }
    public string End { get; }
    public string Pourpose { get; }
    public int StatusId { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }

    public EventResponse(Guid id, string title, string description, bool allDay, string color, string textColor, int unitId, DateTime reservationDate, string start, string end, string pourpose, int statusId)
    {
        Id = id;
        Title = title;
        Description = description;
        AllDay = allDay;
        Color = color;
        TextColor = textColor;
        ReservationByUnitId = unitId;
        ReservationDate = reservationDate;
        Start = start;
        End = end;
        Pourpose = pourpose;
        StatusId = statusId;        
    }
}

namespace Dhanman.MyHome.Application.Contracts.Events;

public class EventResponse
{
    public int Id { get; }
    public string Name { get; }
    public string Description { get; }
    public bool IsFullDay { get; }
    public string BackgroundColor { get; }
    public string TextColor { get; }
    public int UnitId { get; }
    public DateTime ReservationDate { get; }
    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
    public string Pourpose { get; }
    public int StatusId { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }

    public EventResponse(int id, string name, string description, bool isFullDay, string backgroundColor, string textColor, int unitId, DateTime reservationDate, DateTime startDate, DateTime endDate, string pourpose, int statusId)
    {
        Id = id;
        Name = name;
        Description = description;
        IsFullDay = isFullDay;
        BackgroundColor = backgroundColor;
        TextColor = textColor;
        UnitId = unitId;
        ReservationDate = reservationDate;
        StartDate = startDate;
        EndDate = endDate;
        Pourpose = pourpose;
        StatusId = statusId;

    }
}

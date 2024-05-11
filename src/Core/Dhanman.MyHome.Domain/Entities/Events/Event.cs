using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Events;

public class Event : Entity, IAuditableEntity, ISoftDeletableEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool AllDay { get; set; }
    public string Color { get; set; }
    public string TextColor { get; set; }
    public int ReserverdByUnitId { get; set; }
    public DateTime ReservationDate { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
    public string Pourpose { get; set; }
    public int StatusId { get; set; }
    public int BookingFacilitiesId { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }

    public Event(Guid id, string title, string description, bool allDay, string color, string textColor, int reserverdByUnitId, DateTime reservationDate, string start, string end, string pourpose, int statusId, int bookingFacilitiesId)
    {
        Id = id;
        Title = title;
        Description = description;
        AllDay = allDay;
        Color = color;
        TextColor = textColor;
        ReserverdByUnitId = reserverdByUnitId;
        ReservationDate = reservationDate;
        Start = start;
        End = end;
        Pourpose = pourpose;
        StatusId = statusId;
        BookingFacilitiesId = bookingFacilitiesId;

    }

}

using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Events;

public class Event : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
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
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }

    public Event(int id, string name, string description, bool isFullDay,  string backgroundColor, string textColor, int unitId, DateTime reservationDate, DateTime startDate, DateTime endDate, string pourpose, int statusId, int bookingFacilitiesId)
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
        BookingFacilitiesId = bookingFacilitiesId;

    }

}

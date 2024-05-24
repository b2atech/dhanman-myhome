using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool AllDay { get; set; }
    public string Color { get; set; }
    public string TextColor { get; set; }
    public int ReservationByUnitId { get; set; }
    public DateTime ReservationDate { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
    public string Pourpose { get; set; }
    public int StatusId { get; set; }
    public int BookingFacilitiesId { get; set; }
    #endregion

    #region Constructors
    public CreateEventCommand() { }

    public CreateEventCommand(Guid id, string title, string description, bool allDay, string color, string textColor, int reservationByUnitId, DateTime reservationDate, string start, string end, string pourpose, int statusId, int bookingFacilitiesId)
    {
        Id = Id;
        Title = title;
        Description = description;
        AllDay = allDay;
        Color = color;
        TextColor = textColor;
        ReservationByUnitId = reservationByUnitId;
        ReservationDate = reservationDate;
        Start = start;
        End = end;
        Pourpose = pourpose;
        StatusId = statusId;
        BookingFacilitiesId = bookingFacilitiesId;


}
    #endregion
}

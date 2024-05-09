using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommand : ICommand<Result<EntityCreatedResponse>>
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
    #endregion

    #region Constructors
    public CreateEventCommand() { }

    public CreateEventCommand( string name, string description, bool isFullDay, string backgroundColor, string textColor, int unitId, DateTime reservationDate, DateTime startDate, DateTime endDate, string pourpose, int statusId)
    {
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
    #endregion
}

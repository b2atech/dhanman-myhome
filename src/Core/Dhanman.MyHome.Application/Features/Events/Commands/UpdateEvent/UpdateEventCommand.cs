using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Events.Commands.UpdateEvent;
public class UpdateEventCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid CalendarId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int EventTypeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsRecurring { get; set; }
    public int RecurrenceRuleId { get; set; }
    public string Color { get; set; }
    public string TextColor { get; set; }
    #endregion

    #region Constructors
    public UpdateEventCommand(Guid id, Guid companyId, Guid calendarId, string title, string description, int eventTypeId, DateTime startTime, DateTime endTime, bool isRecurring, int recurrenceRuleId, string color, string textColor)
    {
        Id = id;
        CompanyId = companyId;
        CalendarId = calendarId;
        Title = title;
        Description = description;
        EventTypeId = eventTypeId;
        StartTime = startTime;
        EndTime = endTime;
        IsRecurring = isRecurring;
        RecurrenceRuleId = recurrenceRuleId;
        Color = color;
        TextColor = textColor;
    }
    #endregion
}

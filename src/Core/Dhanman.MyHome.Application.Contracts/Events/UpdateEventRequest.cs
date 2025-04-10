namespace Dhanman.MyHome.Application.Contracts.Events;

public class UpdateEventRequest
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
    public UpdateEventRequest() => Title = string.Empty;
    #endregion
}

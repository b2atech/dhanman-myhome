namespace Dhanman.MyHome.Application.Contracts.Events;

public class UpdateEventRequest
{
    #region Properties    
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public int CommunityCalenderId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsRecurring { get; set; }
    public string RecurrenceRule { get; set; }
    public int RecurrenceRuleId { get; set; }
    public DateTime RecurrenceEndDate { get; set; }
    #endregion

    #region Constructors
    public UpdateEventRequest() => Title = string.Empty;
    #endregion
}

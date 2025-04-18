using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Contracts.Events;

public class EventResponse : AuditableEntity
{
    #region Propertries
    public Guid Id { get; set; }
    public Guid CompanyId { get; set; }
    public Guid CalendarId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsRecurring { get; set; }
    public string RecurrenceRule { get; set; }
    #endregion

    #region Constructors
    public EventResponse() { }

    public EventResponse(Guid id, Guid companyId, Guid calendarId, string title, string description, DateTime startTime, DateTime endTime, bool isRecurring, string recurrenceRule, DateTime createdOnUtc, DateTime? modifiedOnUtc, Guid createdBy, Guid? modifiedBy)
        : this(id, companyId, calendarId, title, description, startTime, endTime, isRecurring, recurrenceRule)
    {
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
    }

    public EventResponse(Guid id, Guid companyId, Guid calendarId, string title, string description, DateTime startTime, DateTime endTime, bool isRecurring, string recurrenceRule)
    {
        Id = id;
        CompanyId = companyId;
        CalendarId = calendarId;
        Title = title;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        IsRecurring = isRecurring;
        RecurrenceRule = recurrenceRule;
    }

    #endregion
}

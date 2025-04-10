using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Events;

public class Event : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public Guid CompanyId { get; set; }
    public Guid CalenderId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int EventTypeId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsRecurring { get; set; }
    public int RecurrenceRuleId { get; set; }
    public string Color { get; set; }
    public string TextColor { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructor
    public Event()
    {
        
    }
    public Event(Guid id, Guid companyId, Guid calenderId, string title, string description, int eventTypeId, DateTime startTime, DateTime endTime, bool isRecurring, int recurrenceRuleId, string color, string textColor)
    {
        Id = id;
        CompanyId = companyId;
        CalenderId = calenderId;
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

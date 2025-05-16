using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Events;

public class Event : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public Guid CompanyId { get; set; }
    public int CommunityCalenderId { get; set; }
    public string Title { get; set; }
    public int EventTypeId { get; set; }
    public string Description { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsRecurring { get; set; }
    public string RecurrenceRule { get; set; }
    public int RecurrenceRuleId { get; set; }
    public DateTime? RecurrenceEndDate { get; set; }
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

    public Event( Guid id,Guid companyId, int communityCalenderId, string title,int eventTypeId, string description, DateTime startTime, DateTime endTime, bool isRecurring, string recurrenceRule, int recurrenceRuleId, DateTime? recurrenceEndDate)
    {
        Id = id;
        CompanyId = companyId;
        CommunityCalenderId = communityCalenderId;
        Title = title;
        EventTypeId = eventTypeId;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        IsRecurring = isRecurring;
        RecurrenceRule = recurrenceRule;
        RecurrenceRuleId = recurrenceRuleId;
        RecurrenceEndDate = recurrenceEndDate;
    }
    #endregion
}

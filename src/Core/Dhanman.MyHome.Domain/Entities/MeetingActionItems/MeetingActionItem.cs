using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.MeetingActionItems;

public class MeetingActionItem : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int OccurrenceId { get; set; }        // FK to event_occurrences
    public string ActionDescription { get; set; } // Description of the action item
    public Guid AssignedToUserId { get; set; }    // FK to users
    public DateTime DueDate { get; set; }         // Due date for the action
    public string Status { get; set; }             // Open, In Progress, Done  
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; set; }
    #endregion

    #region Constructor
    public MeetingActionItem(int occurrenceId, string actionDescription, Guid assignedToUserId, DateTime dueDate, string status)
    {
        OccurrenceId = occurrenceId;
        ActionDescription = actionDescription;
        AssignedToUserId = assignedToUserId;
        DueDate = dueDate;
        Status = status;
    }
    #endregion
}

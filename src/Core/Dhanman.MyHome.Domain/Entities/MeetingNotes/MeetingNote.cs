using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.MeetingNotes;

public class MeetingNote : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int OccurrenceId { get; set; }       
    public string NoteText { get; set; } // Description of the action item
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; set; }
    #endregion
}

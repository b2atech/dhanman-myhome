using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.MeetingParticipants;

public class MeetingParticipant : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int OccurrenceId { get; set; }  // FK to event_occurrences
    public Guid UserId { get; set; }        // FK to users
    public string? Role { get; set; }        // Chairperson, Member, Guest, etc.
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
    public MeetingParticipant(int id, int occurrenceId, Guid userId, string? role)
    {
        Id = id;
        OccurrenceId = occurrenceId;
        UserId = userId;
        Role = role;
    }
    #endregion
}

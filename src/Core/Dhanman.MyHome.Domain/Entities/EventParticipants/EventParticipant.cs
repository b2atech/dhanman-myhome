using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.EventParticipants;

public class EventParticipant : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public Guid EventId { get; set; }
    public Guid UserId { get; set; }
    public int EventStatusId { get; set; }
    public bool NotificationSent { get; set; }
    #endregion

    #region Audit Properties
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
    #endregion

    #region Constructor
    public EventParticipant(Guid id, Guid eventId, Guid userId, int eventStatusId, bool notificationSent)
    {
        Id = id;
        EventId = eventId;
        UserId = userId;
        EventStatusId = eventStatusId;
        NotificationSent = notificationSent;
    }
    #endregion
}

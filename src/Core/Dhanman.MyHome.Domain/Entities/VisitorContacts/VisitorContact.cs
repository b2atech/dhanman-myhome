using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VisitorContacts;

public class VisitorContact : EntityLong, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties

    public long Id { get; set; }
    public int VisitorId { get; set; }
    public string ContactNumber { get; set; }
    public bool IsCurrent { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }
    #endregion

    #region Constructor
    public VisitorContact(long id, int visitorId, string contactNumber, bool isCurrent, DateTime createdOnUtc, DateTime? modifiedOnUtc, DateTime? deletedOnUtc, bool isDeleted, Guid createdBy, Guid? modifiedBy)
    {
        Id = id;
        VisitorId = visitorId;
        ContactNumber = contactNumber;
        IsCurrent = isCurrent;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
        DeletedOnUtc = deletedOnUtc;
        IsDeleted = isDeleted;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
    }
    #endregion
}

using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.CommunityCalenders;

public class CommunityCalender : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    public string Color { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructor
    public CommunityCalender()
    {

    }

    public CommunityCalender(int  id, string name, string color, DateTime createdOnUtc, DateTime? modifiedOnUtc, DateTime? deletedOnUtc, bool isDeleted, Guid createdBy, Guid? modifiedBy)
    {
        Id = id;
        Name = name;
        Color = color;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
        DeletedOnUtc = deletedOnUtc;
        IsDeleted = isDeleted;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
    }

    #endregion
}

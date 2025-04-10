using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Calendars;

public class Calendar: Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }    
    public string Description { get; set; }
    public bool IsPublic { get; set; }
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
    public Calendar(Guid id, string name, string description, bool isPublic)
    {
        Id = id;
        Name = name;
        Description = description;
        IsPublic = isPublic;
    }
    #endregion
}

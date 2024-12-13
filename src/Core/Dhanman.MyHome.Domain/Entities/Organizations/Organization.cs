using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Organizations;

public class Organization : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }

    #endregion

    #region Constructors
    public Organization() { }

    public Organization(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}
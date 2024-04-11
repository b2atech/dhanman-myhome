using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Cities;
 
public class City : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; private set; }
    public Guid StateId { get; private set; }
    public string ZipCode { get; private set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }

    #endregion

    #region Constructors
    public City() { }
    public City(Guid id, Guid stateId, string zipCode, string name, Guid createdBy)
    {
        Id = id;
        Name = name;
        StateId = stateId;
        ZipCode = zipCode;
        CreatedBy = createdBy;
    }
    #endregion
}
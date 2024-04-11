using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Countries;
public class Country : Entity, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; private set; }
    public string ISOAlphaCode { get; private set; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }

    #endregion

    #region Constructors
    public Country() { }
    public Country(Guid id, string name, string iSOAlphaCode)
    {
        Id = id;
        Name = name;
        ISOAlphaCode = iSOAlphaCode;
        CreatedOnUtc = DateTime.UtcNow;
    }
    #endregion
}
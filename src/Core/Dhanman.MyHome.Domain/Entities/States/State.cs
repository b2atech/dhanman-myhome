using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.States;

public class State : Entity, IAuditableEntity, ISoftDeletableEntity
{
#region Properties
public string Name { get; private set; }
public Guid CountryId { get; private set; }
public Guid CreatedBy { get; protected set; }
public Guid? ModifiedBy { get; protected set; }
public DateTime CreatedOnUtc { get; }
public DateTime? ModifiedOnUtc { get; }
public DateTime? DeletedOnUtc { get; }
public bool IsDeleted { get; }

#endregion

#region Constructors
public State() { }
public State(Guid id, string name, Guid countryId)
{
    Id = id;
    Name = name;
    CountryId = countryId;
    CreatedOnUtc = DateTime.UtcNow;
}
#endregion
}
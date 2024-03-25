using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Apartments;

public class VisitorType : EntityInt, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    #endregion

    #region Deleteable Properties
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    #endregion

    #region Constructor
    public VisitorType(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}
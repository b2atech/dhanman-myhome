using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.VerificationTypes;

public class VerificationType : EntityInt, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }

    #endregion

    #region Deleteable Properties
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    #endregion

    #region Constructor
    public VerificationType(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}
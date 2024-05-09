using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.ServiceProviderSubTypes;

public class ServiveProviderSubType : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
   
    #region Properties

    public int Id { get; set; }

    public string Name { get; set; }
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
    public ServiveProviderSubType(int id, string name)
    {
        Id = id;
        Name = name;
       
    }

    #endregion

}

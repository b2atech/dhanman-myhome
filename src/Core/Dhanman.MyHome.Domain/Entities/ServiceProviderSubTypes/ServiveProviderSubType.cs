using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.ServiceProviderSubTypes;

public class ServiveProviderSubType : EntityInt, IAuditableEntity, ISoftDeletableEntity
{

    #region Properties

    public int Id { get; set; }
    public int ServiceProviderTypeId { get; }
    public string Name { get; }
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
    public ServiveProviderSubType(int id, int serviceProviderTypeId, string name)
    {
        Id = id;
        ServiceProviderTypeId = serviceProviderTypeId;
        Name = name;
    }

    #endregion

}

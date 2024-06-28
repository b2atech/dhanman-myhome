using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.ServiceProviderVerifications;
public class ServiceProviderVerification : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    public int ServiceProviderId { get; set; }
    public int VerificationTypeId { get; set; }    
    public string Comments { get; set; }
    public bool IsVerified { get; set; }    
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
    public ServiceProviderVerification(int id, string name, int serviceProviderId, int verificationTypeId, string comments, bool isVerified)
    {
        Id = id;
        Name = name;
        ServiceProviderId = serviceProviderId;
        VerificationTypeId = verificationTypeId;
        Comments = comments;
        IsVerified = isVerified;
    }
    #endregion
}
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.ServiceProviderUsers;

public class ServiceProviderUser : EntityInt
{
    #region Properties
    public Guid ServiceProviderId { get; set; }

    public bool IsActive { get; set; }
    #endregion

    #region Constructor
    public ServiceProviderUser(int id, Guid serviceProviderId)
    {
        Id = id;
        ServiceProviderId = serviceProviderId;
        IsActive =
    }
    #endregion
}

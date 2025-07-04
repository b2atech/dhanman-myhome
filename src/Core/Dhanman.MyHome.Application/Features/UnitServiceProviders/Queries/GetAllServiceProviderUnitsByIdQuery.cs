using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.UnitServiceProviders;

namespace Dhanman.MyHome.Application.Features.UnitServiceProviders.Queries;

public class GetAllServiceProviderUnitsByIdQuery : ICacheableQuery<Result<AssignUnitListResponse>>
{

    #region Properties
    public int ServiceProviderId { get; set; }

    #endregion

    #region Constructor
    public GetAllServiceProviderUnitsByIdQuery(int serviceProviderId)
    {
        ServiceProviderId = serviceProviderId;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.ServiceProviderAssignUnits.ServiceProviderAssignUnitsId, "user", ServiceProviderId);
    #endregion
}

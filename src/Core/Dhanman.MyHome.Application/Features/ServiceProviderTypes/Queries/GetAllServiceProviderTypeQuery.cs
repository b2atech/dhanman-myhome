using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.ServiceProviderTypes;

namespace Dhanman.MyHome.Application.Features.ServiceProviderTypes.Queries;

public class GetAllServiceProviderTypeQuery : ICacheableQuery<Result<ServiceProivderTypeListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllServiceProviderTypeQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.ServiceProviderType.ServiceProviderTypeList, "user", "");
    #endregion 
}

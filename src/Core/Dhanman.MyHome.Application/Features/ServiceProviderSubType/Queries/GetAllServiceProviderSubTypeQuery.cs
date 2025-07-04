using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.ServiceProviderSubTypes;

namespace Dhanman.MyHome.Application.Features.ServiceProviderSubType.Queries;

public class GetAllServiceProviderSubTypeQuery : ICacheableQuery<Result<ServiceProivderSubTypeListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllServiceProviderSubTypeQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.ServiceProviderSubType.ServiceProviderSubTypeList, "user", "");
    #endregion 

}

using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Queries;

public class GetAllServiceProviderNamesQuery : ICacheableQuery<Result<ServiceProviderNameListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllServiceProviderNamesQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.ServiceProviders.ServiceProviderList, "user", "");
    #endregion 

}
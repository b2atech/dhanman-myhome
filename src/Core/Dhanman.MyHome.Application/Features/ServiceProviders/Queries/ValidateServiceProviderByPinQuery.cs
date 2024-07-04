using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.ServiceProviders;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Queries;

public class ValidateServiceProviderByPinQuery : ICacheableQuery<Result<ServiceProviderValidationResponse>>
{
    #region Properties     
    public string Pin { get; set; }

    #endregion

    #region Constructors
    public ValidateServiceProviderByPinQuery(string pin)
    {
        Pin = pin;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.ServiceProviders.ServiceProviderList, "user", "");
    #endregion 
}

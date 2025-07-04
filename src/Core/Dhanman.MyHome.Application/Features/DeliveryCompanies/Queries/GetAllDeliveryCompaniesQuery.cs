using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.DeliveryCompanies;

namespace Dhanman.MyHome.Application.Features.DeliveryCompanies.Queries;

public class GetAllDeliveryCompaniesQuery : ICacheableQuery<Result<DeliveryCompanyListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllDeliveryCompaniesQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.DeliveryCompanies.DeliveryCompanyList, "user", "");
    #endregion

}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.ResidentRequests;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Queries;

public class GetAllResidentRequestsQuery : ICacheableQuery<Result<ResidentRequestListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllResidentRequestsQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.ResidentRequests.ResidentRequestList, "user");
    #endregion

}
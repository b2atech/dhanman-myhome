using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Residents;

namespace Dhanman.MyHome.Application.Features.Residents.Queries;

public class GetAllResidentsQuery : ICacheableQuery<Result<ResidentListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllResidentsQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Residents.ResidentList, "user", "");
    #endregion

}
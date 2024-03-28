using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Residents;

namespace Dhanman.MyHome.Application.Features.Residents.Queries;

public class GetAllResidentNamesQuery : ICacheableQuery<Result<ResidentNameListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllResidentNamesQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Residents.ResidentList, "user");
    #endregion 

}
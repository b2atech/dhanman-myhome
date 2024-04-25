using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetAllUnitDetailsQuery : ICacheableQuery<Result<UnitDetailListResponse>>
{
    #region Properties
    #endregion

    #region Constructors
    public GetAllUnitDetailsQuery()
    {
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Units.UnitList, "user");
    #endregion 

}
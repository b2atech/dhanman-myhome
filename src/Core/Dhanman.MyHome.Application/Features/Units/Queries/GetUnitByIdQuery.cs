using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetUnitByIdQuery : ICacheableQuery<Result<UnitResponse>>
{
    #region Properties
    public int UnitId { get; set; }

    #endregion

    #region Constructor
    public GetUnitByIdQuery(int unitId)
    {
        UnitId = unitId;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Units.UnitById, "user", UnitId);
    #endregion
}

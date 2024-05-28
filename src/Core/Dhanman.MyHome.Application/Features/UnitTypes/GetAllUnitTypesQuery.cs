using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.UnitTypes;

namespace Dhanman.MyHome.Application.Features.UnitTypes
{
    public class GetAllUnitTypesQuery : ICacheableQuery<Result<UnitTypeListResponse>>
    {
        #region Properties     
        #endregion

        #region Constructors
        public GetAllUnitTypesQuery() { }
        #endregion

        #region Methodes
        public string GetCacheKey() => string.Format(CacheKeys.OccupancyTypes.OccupancyTypeList, "user", "");
        #endregion
    }
}

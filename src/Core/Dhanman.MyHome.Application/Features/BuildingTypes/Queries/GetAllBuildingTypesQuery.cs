using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.BuildingTypes;

namespace Dhanman.MyHome.Application.Features.BuildingTypes.Queries;

public class GetAllBuildingTypesQuery : ICacheableQuery<Result<BuildingTypeListResponse>>
{
    #region MyRegion
    public GetAllBuildingTypesQuery()
    {
            
    }
    #endregion
    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Buildings.BuildingList, "user", "");
    #endregion

}

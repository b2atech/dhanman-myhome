using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Buildings;

namespace Dhanman.MyHome.Application.Features.Buildings.Queries;

public class GetBuildingByBuildingIdQuery : ICacheableQuery<Result<BuildingListResponse>>
{
    #region Properties    
    public int BuildingId { get; set; }
    #endregion

    #region Constructors
    public GetBuildingByBuildingIdQuery(int buildingId)
    {
        BuildingId = buildingId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Buildings.BuildingByBuildingId, "user", "");
    #endregion 

}
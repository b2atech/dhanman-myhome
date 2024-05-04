using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetAllUnitDetailsQuery : ICacheableQuery<Result<UnitDetailListResponse>>
{
    #region Properties
    public int BuildingId { get; set; }
    public int OccupancyTypeId { get; set; }
    #endregion

    #region Constructors
    public GetAllUnitDetailsQuery(int buildingId, int occupancyTypeId)
    {
        BuildingId = buildingId;
        OccupancyTypeId = occupancyTypeId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Units.UnitList, "user", BuildingId);
    #endregion 

}
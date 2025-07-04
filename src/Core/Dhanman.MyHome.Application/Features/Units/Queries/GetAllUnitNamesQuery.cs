using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetAllUnitNamesQuery : ICacheableQuery<Result<UnitNameListResponse>>
{
    #region Properties  
    public Guid ApartmentId { get; set; }
    public int BuildingId { get; set; }
    public int FloorId { get; set; }
    #endregion

    #region Constructors
    public GetAllUnitNamesQuery(Guid apartmentId, int buildingId, int floorId) 
    {
        ApartmentId = apartmentId;
        BuildingId = buildingId;
        FloorId = floorId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Units.UnitList, "user", "");
    #endregion 

}
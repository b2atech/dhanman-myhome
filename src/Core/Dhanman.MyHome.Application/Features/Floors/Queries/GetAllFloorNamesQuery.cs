using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Floors;

namespace Dhanman.MyHome.Application.Features.Floors.Queries;

public class GetAllFloorNamesQuery : ICacheableQuery<Result<FloorNameListResponse>>
{
    #region Properties
    public Guid ApartmentId { get; set; }
    public int BuildingId { get; set; }
    #endregion

    #region Constructors
    public GetAllFloorNamesQuery(Guid apartmentId, int buildingId)
    {
        ApartmentId = apartmentId;
        BuildingId = buildingId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Floors.FloorNameList, "user", ApartmentId);
    #endregion 

}
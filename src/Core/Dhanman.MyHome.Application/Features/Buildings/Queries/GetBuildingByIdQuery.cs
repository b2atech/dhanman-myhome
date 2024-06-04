using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Buildings;

namespace Dhanman.MyHome.Application.Features.Buildings.Queries;

public class GetBuildingByIdQuery : ICacheableQuery<Result<BuildingResponse>>
{
    #region Properties    
    public int BuildingId { get; set; }
    #endregion

    #region Constructors
    public GetBuildingByIdQuery(int buildingId)
    {
        BuildingId = buildingId;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Buildings.BuildingById, "user", BuildingId);
    #endregion 

}
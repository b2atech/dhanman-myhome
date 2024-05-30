using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Floors;

namespace Dhanman.MyHome.Application.Features.Floors.Queries;

public class GetFloorByFloorIdQuery : ICacheableQuery<Result<FloorListResponse>>
{
    #region Properties
    public int FloorId { get; set; }
    #endregion

    #region Constructors
    public GetFloorByFloorIdQuery(int floorId) => FloorId = floorId;
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Floors.FloorById, "user", FloorId);
    #endregion
}

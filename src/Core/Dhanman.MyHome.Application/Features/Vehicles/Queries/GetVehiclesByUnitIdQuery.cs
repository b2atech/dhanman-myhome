using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Vehicles.Queries;

public class GetVehiclesByUnitIdQuery : ICacheableQuery<Result<BasicVehicleInfoListResponse>>
{
    # region Properties
    public int UnitId { get; }

    #endregion 

    #region Constructor
    public GetVehiclesByUnitIdQuery(int unitId)
    {
        UnitId = unitId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Vehicles.VehicleList, "user", "");
    #endregion
}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Vehicles;

namespace Dhanman.MyHome.Application.Features.Vehicles.Queries;

public class GetAllVehiclesQuery : ICacheableQuery<Result<VehicleListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllVehiclesQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Vehicles.VehicleList, "user", "");
    #endregion

}
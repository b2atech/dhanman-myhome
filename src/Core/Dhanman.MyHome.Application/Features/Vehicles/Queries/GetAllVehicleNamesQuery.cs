using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Vehicles;

namespace Dhanman.MyHome.Application.Features.Vehicles.Queries;

public class GetAllVehicleNamesQuery : ICacheableQuery<Result<VehicleNameListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllVehicleNamesQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Vehicles.VehicleList, "user");
    #endregion 

}
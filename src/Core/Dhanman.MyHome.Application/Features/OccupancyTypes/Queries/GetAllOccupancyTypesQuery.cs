using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.OccupancyTypes;

namespace Dhanman.MyHome.Application.Features.OccupancyTypes.Queries;

public class GetAllOccupancyTypesQuery : ICacheableQuery<Result<OccupancyTypeListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllOccupancyTypesQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.OccupancyTypes.OccupancyTypeList, "user", "");
    #endregion 

}
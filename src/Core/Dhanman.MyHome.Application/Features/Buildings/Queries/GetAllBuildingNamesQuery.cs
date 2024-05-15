using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Buildings;

namespace Dhanman.MyHome.Application.Features.Buildings.Queries;

public class GetAllBuildingNamesQuery : ICacheableQuery<Result<BuildingNameListResponse>>
{
    #region Properties     
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructors
    public GetAllBuildingNamesQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Buildings.BuildingList, "user", "");
    #endregion 

}
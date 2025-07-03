using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Buildings;

namespace Dhanman.MyHome.Application.Features.Buildings.Queries;
 
public class GetAllBuildingsQuery : ICacheableQuery<Result<BuildingListResponse>>
{
    #region Properties    
    public Guid ApartmentId { get; }
    #endregion

    #region Constructors
    public GetAllBuildingsQuery(Guid apartmentId) => ApartmentId = apartmentId;
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Buildings.BuildingList, "user", ApartmentId);
    #endregion 

}
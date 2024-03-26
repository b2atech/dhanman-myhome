using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Apartments;

namespace Dhanman.MyHome.Application.Features.Apartments.Queries;
 
public class GetAllBuildingsQuery : ICacheableQuery<Result<BuildingListResponse>>
{
    #region Properties
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructors
    public GetAllBuildingsQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Buildings.BuildingList, "user", ApartmentId);
    #endregion 

}
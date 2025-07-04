using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Floors;

namespace Dhanman.MyHome.Application.Features.Floors.Queries;

public class GetAllFloorsQuery : ICacheableQuery<Result<FloorListResponse>>
{
    #region Properties
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructors
    public GetAllFloorsQuery(Guid apartmentId)=> ApartmentId = apartmentId;
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Floors.FloorList, "user", ApartmentId);
    #endregion 

}
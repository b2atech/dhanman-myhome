using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.ResidentRequests;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Queries;

public class GetAllResidentRequestsQuery : ICacheableQuery<Result<ResidentRequestListResponse>>
{
    #region Properties    
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructors
    public GetAllResidentRequestsQuery(Guid apartmentId) => ApartmentId = apartmentId;
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.ResidentRequests.ResidentRequestList, "user", "");
    #endregion

}
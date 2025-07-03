using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.MemberRequests;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Queries;

public class GetAllMemberRequestsQuery : ICacheableQuery<Result<MemberRequestListResponse>>
{
    #region Properties    
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructors
    public GetAllMemberRequestsQuery(Guid apartmentId) => ApartmentId = apartmentId;
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.MemberRequests.MemberRequestList, "user", "");
    #endregion

}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.ApprovedVisitors;

namespace Dhanman.MyHome.Application.Features.ApprovedVisitors.Queries;

public class GetApprovedVisitorInfoQuery : ICacheableQuery<Result<ApprovedInfoByIdResponse>>
{
    #region Properties
    public int ApprovedVisitorId { get; }
    #endregion

    #region Constructors

    public GetApprovedVisitorInfoQuery(int approvedId)
    {
        ApprovedVisitorId = approvedId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.VisitorApprovals.CacheKeyPrefix, "visitor-approval-id", ApprovedVisitorId);
    #endregion

}

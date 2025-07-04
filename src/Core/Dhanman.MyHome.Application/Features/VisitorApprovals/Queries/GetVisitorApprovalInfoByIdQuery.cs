using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.VisitorApprovals;

namespace Dhanman.MyHome.Application.Features.VisitorApprovals.Queries;

public class GetVisitorApprovalInfoByIdQuery : ICacheableQuery<Result<VisitorApprovalsInfoByIdResponse>>
{
    #region Properties
    public int VisitorApprovalId { get; }
    #endregion

    #region Constructors

    public GetVisitorApprovalInfoByIdQuery(int visitorApprovalId)
    {
        VisitorApprovalId = visitorApprovalId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.VisitorApprovals.CacheKeyPrefix, "visitor-approval-id", VisitorApprovalId);
    #endregion
}

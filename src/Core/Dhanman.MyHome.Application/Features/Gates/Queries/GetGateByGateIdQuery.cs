using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Gates;

namespace Dhanman.MyHome.Application.Features.Gates.Queries;

public class GetGateByGateIdQuery : ICacheableQuery<Result<GateListResponse>>
{
    #region Properties
    public int GateId { get; set; }
    #endregion
    #region Constructors
    public GetGateByGateIdQuery(int gateId)
    {
        GateId = gateId;
    }
    #endregion
    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Gates.GateByGateId, "user", "");
    #endregion 

}

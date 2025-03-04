using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Gates;

namespace Dhanman.MyHome.Application.Features.GateTypes.Queries;

public class GetAllGateTypeQuery: ICacheableQuery<Result<GateTypeListResponse>>
{
    
    #region Constructors
    public GetAllGateTypeQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.GateTypes.GateTypeList, "user", "");
    #endregion
}

using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Visitors;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries;

public class GetVisitorTypesQuery : ICacheableQuery<Result<VisitorTypeListResponse>>
{

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Visitors.VisitorList, "user", "");
    #endregion 

}
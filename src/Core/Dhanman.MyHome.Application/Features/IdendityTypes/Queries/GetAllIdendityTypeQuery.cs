using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.IdendityTypes;

namespace Dhanman.MyHome.Application.Features.IdendityTypes.Queries;

public class GetAllIdendityTypeQuery: ICacheableQuery<Result<IdentityTypeListResponse>>
{
    
    #region Constructors
    public GetAllIdendityTypeQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.IdentiyTypes.IdentityTypeList, "user", "");
    #endregion
}

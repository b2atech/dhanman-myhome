using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.CommunityCalenders;

namespace Dhanman.MyHome.Application.Features.CommunityCalenders.Queries;

public class GetAllCommunityCalenderNameQuery : ICacheableQuery<Result<CommunityCalenderNameListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllCommunityCalenderNameQuery()
    {
        
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.CommunityCalenders.CommunityCalenderList, "user", "");
    #endregion
}

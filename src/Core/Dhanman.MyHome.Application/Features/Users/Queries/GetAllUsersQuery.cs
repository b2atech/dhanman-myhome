using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Users;

namespace Dhanman.MyHome.Application.Features.Users.Queries;

public class GetAllUsersQuery : ICacheableQuery<Result<UserListResponse>>
{
    #region Properties     
    #endregion

    #region Constructors
    public GetAllUsersQuery() { }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Users.UserList, "user", "");
    #endregion

}

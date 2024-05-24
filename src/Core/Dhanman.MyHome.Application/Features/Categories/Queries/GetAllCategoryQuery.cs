using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Catergories;
using Dhanman.MyHome.Application.Contracts.Events;

namespace Dhanman.MyHome.Application.Features.Categories.Queries;

public class GetAllCategoryQuery : ICacheableQuery<Result<CategoryListResponse>>
{
    #region Properties 
    #endregion

    #region Constructors
    public GetAllCategoryQuery()
    {
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Category.CategoryList, "user", "");
    #endregion
}
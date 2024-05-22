using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Catergories;
using Dhanman.MyHome.Application.Contracts.SubCategories;

namespace Dhanman.MyHome.Application.Features.SubCategories;

public class GetAllSubCategoryQuery : ICacheableQuery<Result<SubCategoryListResponse>>
{
    #region Properties 
    #endregion

    #region Constructors
    public GetAllSubCategoryQuery()
    {
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.SubCategory.SubCategoryList, "user", "");
    #endregion
}
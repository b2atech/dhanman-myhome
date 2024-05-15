using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Apartments;

namespace Dhanman.MyHome.Application.Features.Apartments.Queries;

public class GetAllApartmentNamesQuery : ICacheableQuery<Result<ApartmentNameListResponse>>
{

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Apartments.ApartmentNameList, "user", "");
    #endregion 

}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Apartments;

namespace Dhanman.MyHome.Application.Features.Apartments.Queries;
 
public class GetAllApartmentsQuery : ICacheableQuery<Result<ApartmentListResponse>>
{
  
    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Apartments.ApartmentList, "user", "");
    #endregion 

}
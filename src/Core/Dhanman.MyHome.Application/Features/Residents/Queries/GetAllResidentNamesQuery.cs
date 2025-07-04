using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Residents;

namespace Dhanman.MyHome.Application.Features.Residents.Queries;

public class GetAllResidentNamesQuery : ICacheableQuery<Result<ResidentNameListResponse>>
{
    #region Properties  
    public Guid ApartmentId { get; }
    #endregion
    #region Constructors
    public GetAllResidentNamesQuery(Guid apartmentId) => ApartmentId = apartmentId;
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Residents.ResidentList, "user", "");
    #endregion 

}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Queries;
public class GetAllUnitsQuery : ICacheableQuery<Result<UnitListResponse>>
{
    #region Properties 
    public Guid ApartmentId { get; }
    #endregion

    #region Constructors
    public GetAllUnitsQuery(Guid apartmentId) => ApartmentId = apartmentId;
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Units.UnitList, "user", ApartmentId);
    #endregion 

}
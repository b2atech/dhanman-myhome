using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Queries;
public class GetAllUnitsQuery : ICacheableQuery<Result<UnitListResponse>>
{
    #region Properties 
    public Guid ApartmentId { get; }
    public bool IsGetForAllOrganization { get; }
    #endregion

    #region Constructors
    public GetAllUnitsQuery(Guid apartmentId, bool isGetForAllOrganization)
    {
        ApartmentId = apartmentId;
        IsGetForAllOrganization = isGetForAllOrganization;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Units.UnitList, "user", ApartmentId);
    #endregion 

}
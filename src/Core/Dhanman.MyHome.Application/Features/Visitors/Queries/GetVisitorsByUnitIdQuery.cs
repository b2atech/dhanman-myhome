using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Visitors;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries;

public class GetVisitorsByUnitIdQuery : ICacheableQuery<Result<VisitorsByUnitIdListResponse>>
{
    #region Properties  
    public Guid ApartmentId { get; set; }
    public int UnitId { get; set; }
    #endregion

    #region Constructors
    public GetVisitorsByUnitIdQuery(Guid apartmentId, int unitId)
    {
        ApartmentId = apartmentId;
        UnitId = unitId; 
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Visitors.VisitorList, "user", "");
    #endregion 

}
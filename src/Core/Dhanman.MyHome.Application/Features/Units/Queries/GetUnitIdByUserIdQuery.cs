using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Units;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetUnitIdByUserIdQuery : ICacheableQuery<Result<GetUnitIdbyUserIdResponse>>
{
    #region Properties
    public Guid UserId { get; set; }
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructor
    public GetUnitIdByUserIdQuery(Guid userId, Guid apartmentId)
    {
        UserId = userId;
        ApartmentId = apartmentId;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Units.UnitIdByUserId, "user", UserId);
    #endregion
}

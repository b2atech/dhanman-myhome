using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Gates;

namespace Dhanman.MyHome.Application.Features.Gates.Queries;

public class GetAllGatesQuery : ICacheableQuery<Result<GateListResponse>>
{
    #region Properties
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructors
    public GetAllGatesQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Gates.GateList, "user", "");
    #endregion 

}
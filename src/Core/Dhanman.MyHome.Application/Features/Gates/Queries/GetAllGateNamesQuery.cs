using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Gates;

namespace Dhanman.MyHome.Application.Features.Gates.Queries;

public class GetAllGateNamesQuery : ICacheableQuery<Result<GateNameListResponse>>
{
    #region Properties
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructors
    public GetAllGateNamesQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Gates.GateNameList, "user", "");
    #endregion 

}
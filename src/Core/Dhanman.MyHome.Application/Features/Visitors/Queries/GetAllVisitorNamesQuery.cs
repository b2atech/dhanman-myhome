using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Visitors;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries;

public class GetAllVisitorNamesQuery : ICacheableQuery<Result<VisitorNameListResponse>>
{
    #region Properties   
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructors
    public GetAllVisitorNamesQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Visitors.VisitorList, "user", "");
    #endregion 

}
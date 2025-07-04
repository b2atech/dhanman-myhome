using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.VisitorLogs;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Queries;

public class GetAllVisitorLogsQuery : ICacheableQuery<Result<AllVisitorLogListResponse>>
{
    #region Properties  
    public Guid ApartmentId { get; set; }
    public DateTime Date { get; set; }
    #endregion

    #region Constructors
    public GetAllVisitorLogsQuery(Guid apartmentId, DateTime date)
    {
        ApartmentId = apartmentId;
        Date = date;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Visitors.VisitorLogList, "user", "");
    #endregion 

}
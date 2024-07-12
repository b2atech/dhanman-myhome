using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.VisitorLogs;

namespace Dhanman.MyHome.Application.Features.VisitorLogs.Queries;

public class GetAllVisitorLogsQuery : ICacheableQuery<Result<VisitorLogListResponse>>
{
    #region Properties  
    public Guid ApartmentId { get; set; }
    public int VisitorId { get; set; }
    public int VisitorTypeId { get; set; }
    #endregion

    #region Constructors
    public GetAllVisitorLogsQuery(Guid apartmentId, int visitorId, int visitorTypeId)
    {
        ApartmentId = apartmentId;
        VisitorId = visitorId;
        VisitorTypeId = visitorTypeId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Visitors.VisitorLogList, "user", "");
    #endregion 

}
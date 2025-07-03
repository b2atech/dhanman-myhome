using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Events;

namespace Dhanman.MyHome.Application.Features.Events.Queries;

public class GetAllEventsQuery : ICacheableQuery<Result<EventListResponse>>
{
    #region Properties 
    public Guid CompanyId { get; set; }    
    #endregion

    #region Constructors
    public GetAllEventsQuery(Guid companyId)
    {        
        CompanyId = companyId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Events.EventList, "user", "");
    #endregion
}

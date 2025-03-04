using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.TicketStatuses;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetTicketStatusesQuery : ICacheableQuery<Result<TicketStatusListResponse>>
{

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Tickets.TicketList, "user", "");
    #endregion 

}
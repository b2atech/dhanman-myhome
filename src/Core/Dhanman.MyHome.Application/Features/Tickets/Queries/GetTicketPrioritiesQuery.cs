using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.TicketPriorities;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetTicketPrioritiesQuery : ICacheableQuery<Result<TicketPriorityListResponse>>
{

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Tickets.TicketList, "user", "");
    #endregion 

}
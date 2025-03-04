using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.TicketCatetories;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetTicketCatetoriesQuery : ICacheableQuery<Result<TicketCatetoryListResponse>>
{

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Tickets.TicketList, "user", "");
    #endregion 

}
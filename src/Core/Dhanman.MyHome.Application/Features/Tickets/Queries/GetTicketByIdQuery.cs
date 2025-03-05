using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Tickets;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetTicketByIdQuery : ICacheableQuery<Result<TicketResponse>>
{
    #region Properties    
    public Guid TicketId { get; set; }
    #endregion

    #region Constructors
    public GetTicketByIdQuery(Guid ticketId)
    {
        TicketId = ticketId;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Tickets.TicketById, "user", TicketId);
    #endregion 

}
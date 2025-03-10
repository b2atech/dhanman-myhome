using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Tickets;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;
 
public class GetAllTicketsQuery : ICacheableQuery<Result<TicketListResponse>>
{
    #region Properties  
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructors
    public GetAllTicketsQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Tickets.TicketList, "user", "");
    #endregion

}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Events;

namespace Dhanman.MyHome.Application.Features.Events.Queries;

public class GetAllEventsQuery : ICacheableQuery<Result<EventListResponse>>
{
    #region Properties 
    public int BookingFacilitiesId { get; set; }
    public Guid CompanyId { get; set; }    
    #endregion

    #region Constructors
    public GetAllEventsQuery(Guid companyId, int bookingFacilitiesId)
    {        
        CompanyId = companyId;
        BookingFacilitiesId = bookingFacilitiesId;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.Events.EventList, "user", "");
    #endregion
}

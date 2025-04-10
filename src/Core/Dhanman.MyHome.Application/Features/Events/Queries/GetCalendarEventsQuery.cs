using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Events;

namespace Dhanman.MyHome.Application.Features.Events.Queries;

public class GetCalendarEventsQuery : ICacheableQuery<Result<EventListResponse>>
{
    #region Properties
    public Guid CalendarId { get; set; }
    public string View { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    #endregion

    #region Constructor
    public GetCalendarEventsQuery(Guid calendarId, string view, DateTime? startDate, DateTime? endDate)
    {
        CalendarId = calendarId;
        View = view;
        StartDate = startDate;
        EndDate = endDate;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Events.EventList, "user", CalendarId);
    #endregion
}

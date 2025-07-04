using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Events;

namespace Dhanman.MyHome.Application.Features.Events.Queries;

public class GetCalendarEventsQuery : ICacheableQuery<Result<EventListResponse>>
{
    #region Properties
    public List<int> CommunityCalenderIds { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    #endregion

    #region Constructor
    public GetCalendarEventsQuery(List<int> communityCalenderIds, DateTime? startDate, DateTime? endDate)
    {
        CommunityCalenderIds = communityCalenderIds;

        StartDate = startDate;
        EndDate = endDate;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Events.EventList, "user", CommunityCalenderIds);
    #endregion
}

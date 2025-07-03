using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.MeetingDetails;

namespace Dhanman.MyHome.Application.Features.MeetingDetails.Queries;

public class GetMeetingDetailsQuery : ICacheableQuery<Result<MeetingDetailsDto>>
{
    #region Properties 
    public Guid EventId { get; set; }
    public DateOnly OccurrenceDate { get; set; }
    #endregion

    #region Constructors
    public GetMeetingDetailsQuery(Guid eventId, DateOnly occurrenceDate)
    {
        EventId = eventId;
        OccurrenceDate = occurrenceDate;
    }
    #endregion

    #region Methodes
    public string GetCacheKey() => string.Format(CacheKeys.MeetingDetails.CacheKeyPrefix, "meetingDetails");
    #endregion
}
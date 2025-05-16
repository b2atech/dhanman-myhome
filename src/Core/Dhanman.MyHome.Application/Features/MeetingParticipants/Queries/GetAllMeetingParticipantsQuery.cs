using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Users;

namespace Dhanman.MyHome.Application.Features.MeetingParticipants.Queries;
 public class GetAllMeetingParticipantsQuery : ICacheableQuery<Result<UserNameListResponse>>
{
    #region Properties
    public int OccurrenceId { get; }    
    #endregion

    #region Constructor  
    public GetAllMeetingParticipantsQuery(int occurrenceId)
    {
        OccurrenceId = occurrenceId;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.Events.EventList, "user", OccurrenceId);
    #endregion
}
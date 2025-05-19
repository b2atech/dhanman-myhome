using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.MeetingAgendaItems;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Query;

public sealed class GetMeetingAgendaItemByIdQuery : ICacheableQuery<Result<MeetingAgendaItemResponse>>
{
    #region Properties    
    public int MeetingAgendaItemId { get; set; }
    #endregion

    #region Constructors
    public GetMeetingAgendaItemByIdQuery(int meetingAgendaItemId)
    {
        MeetingAgendaItemId = meetingAgendaItemId;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.MeetingAgendaItems.MeetingAgendaItemById, "user", MeetingAgendaItemId);
    #endregion

}

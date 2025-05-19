using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.EventOccurrences;

namespace Dhanman.MyHome.Application.Features.EventOccurrences.Queries;

public class GetEventOccurrenceByIdQuery : ICacheableQuery<Result<EventOccurrenceResponse>>
{
    #region Properties    
    public int EventOccurrenceId { get; set; }
    #endregion

    #region Constructors
    public GetEventOccurrenceByIdQuery(int eventOccurrenceId)
    {
        EventOccurrenceId = eventOccurrenceId;
    }
    #endregion

    #region Methods
    public string GetCacheKey() => string.Format(CacheKeys.EventOccurrences.EventOccurrenceById, "user", EventOccurrenceId);
    #endregion

}
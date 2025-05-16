using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Events;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Events;
using Dhanman.MyHome.Domain.Entities.EventTypes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dhanman.MyHome.Application.Features.Events.Queries;

public class GetCalendarEventsQueryHandler : IQueryHandler<GetCalendarEventsQuery, Result<EventListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public GetCalendarEventsQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    #endregion

    #region Methods
    public async Task<Result<EventListResponse>> Handle(GetCalendarEventsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {

                var events = await (
                                        from e in _dbContext.Set<Event>()
                                        join et in _dbContext.SetInt<EventType>()
                                        on e.EventTypeId equals et.Id
                                        where
                                        // e.CompanyId == query.CompanyId &&
                                        request.CommunityCalenderIds.Contains(e.CommunityCalenderId)
                                        select new EventResponse(
                                               e.Id,
                                               e.CompanyId,
                                               e.CommunityCalenderId,
                                               e.Title,
                                               e.EventTypeId,
                                               et.Name,
                                               e.Description,
                                               e.StartTime,
                                               e.EndTime,
                                               e.IsRecurring,
                                               e.RecurrenceRule,
                                               e.RecurrenceRuleId,
                                               e.RecurrenceEndDate
                                                                   )
                                           ).ToListAsync(cancellationToken);


                return Result.Success(new EventListResponse(events));
            });
    }
    #endregion


}

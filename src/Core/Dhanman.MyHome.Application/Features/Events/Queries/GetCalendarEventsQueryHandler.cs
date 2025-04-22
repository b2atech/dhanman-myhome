using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Events;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Events;
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
                var eventsQuery = _dbContext.Set<Event>()
                    .AsNoTracking()
                    .Where(e => e.CommunityCalenderId == request.CommunityCalenderId);

                switch (request.View.ToLower())
                {
                    case "month":
                        if (request.StartDate.HasValue)
                        {
                            var startOfMonth = new DateTime(request.StartDate.Value.Year, request.StartDate.Value.Month, 1);
                            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                            eventsQuery = eventsQuery.Where(e => e.StartTime >= startOfMonth && e.StartTime <= endOfMonth);
                        }
                        break;

                    case "week":
                        if (request.StartDate.HasValue)
                        {
                            var startOfWeek = request.StartDate.Value.AddDays(-(int)request.StartDate.Value.DayOfWeek);
                            var endOfWeek = startOfWeek.AddDays(7).AddDays(-1);
                            eventsQuery = eventsQuery.Where(e => e.StartTime >= startOfWeek && e.StartTime <= endOfWeek);
                        }
                        break;

                    case "day":
                        if (request.StartDate.HasValue)
                        {
                            var startOfDay = request.StartDate.Value.Date;
                            var endOfDay = startOfDay.AddDays(1).AddSeconds(-1);
                            eventsQuery = eventsQuery.Where(e => e.StartTime >= startOfDay && e.StartTime <= endOfDay);
                        }
                        break;

                    case "list":
                        break;
                }

                var events = await eventsQuery
                    .Select(e => new EventResponse(
                        e.Id,
                        e.CompanyId,
                        e.CommunityCalenderId,
                        e.Title,
                        e.Description,
                        e.StartTime,
                        e.EndTime,
                        e.IsRecurring,
                        e.RecurrenceRule,
                        e.RecurrenceRuleId,
                        e.RecurrenceEndDate
                    ))
                    .ToListAsync(cancellationToken);

                return Result.Success(new EventListResponse(events));
            });
    }
    #endregion

  
}

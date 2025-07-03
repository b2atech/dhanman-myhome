using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Events;
using Dhanman.MyHome.Domain.Entities.Events;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Application.Constants.Enums;
using Dhanman.MyHome.Domain.Entities.EventTypes;


namespace Dhanman.MyHome.Application.Features.Events.Queries;
public class GetEventByIdQueryHandler : IQueryHandler<GetEventByIdQuery, Result<EventResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetEventByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<EventResponse>> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
          .Ensure(query => query != null, Errors.General.EntityNotFound)
          .Bind(async query =>
          {
              var result = await (
                                from e in _dbContext.Set<Event>().AsNoTracking()
                                join et in _dbContext.SetInt<EventType>() on e.EventTypeId equals et.Id
                                where e.Id == query.Id
                                select new EventResponse(
                                                e.Id,
                                                e.CompanyId,
                                                e.CommunityCalenderId,
                                                e.Title,
                                                e.EventTypeId,
                                                et.Name,              // EventTypeName from joined table
                                                e.Description,
                                                e.StartTime,
                                                e.EndTime,
                                                e.IsRecurring,
                                                e.RecurrenceRule,
                                                e.RecurrenceRuleId,
                                                e.RecurrenceEndDate
                                                        )
                                ).FirstOrDefaultAsync(cancellationToken);

              return result;
          }
          );
    }
    #endregion
}

using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Events;
using Dhanman.MyHome.Domain.Entities.Events;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Application.Constants.Enums;


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
                return await _dbContext.Set<Event>()
                  .AsNoTracking()
                  .Where(e => e.Id == query.Id)
                  .Select(e => new EventResponse(
                          e.Id,
                          e.CompanyId,
                          e.CommunityCalenderId,
                          e.Title,
                          e.Description,
                          e.StartTime,
                          e.EndTime,
                          e.IsRecurring,
                          GetRecurringRule(e.RecurrenceRuleId)
                         ))
                  .FirstOrDefaultAsync(cancellationToken);
              });
    }

    private static string GetRecurringRule(int recurringRuleId)
    {
        switch (recurringRuleId)
        {
            case 0:
                return "DAILY"; 
            case 1:
                return "WEEKLY";
            case 2:
                return "MONTHLY";
            case 3:
                return "QUARTERLY";
            case 4:
                return "HALF_YEARLY";
            case 5:
                return "YEARLY"; 
            default:
                return "Invalid Recurrence Rule";
        }
    }

    #endregion
}

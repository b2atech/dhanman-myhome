using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Events;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Events;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Events.Queries;

public class GetAllEventsQueryHandler : IQueryHandler<GetAllEventsQuery, Result<EventListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllEventsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<EventListResponse>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var residents = await _dbContext.SetInt<Event>()
                  .AsNoTracking()
                  .Select(e => new EventResponse(
                          e.Id,
                          e.Name,
                          e.Description,
                          e.IsFullDay,
                          e.BackgroundColor,
                          e.TextColor,
                          e.UnitId,
                          e.ReservationDate,
                          e.StartDate,
                          e.EndDate,
                          e.Pourpose,
                          e.StatusId
                          ))
                  .ToListAsync(cancellationToken);

                  var listResponse = new EventListResponse(residents);

                  return listResponse;
              });
    }
    #endregion
}

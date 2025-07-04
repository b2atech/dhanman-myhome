using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.TicketPriorities;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.TicketPriorities;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetTicketPrioritiesQueryHandler : IQueryHandler<GetTicketPrioritiesQuery, Result<TicketPriorityListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetTicketPrioritiesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<TicketPriorityListResponse>> Handle(GetTicketPrioritiesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var ticketPriorities = await _dbContext.SetInt<TicketPriority>()
                  .AsNoTracking()
                  .Select(e => new TicketPriorityResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new TicketPriorityListResponse(ticketPriorities);

                  return listResponse;
              });
    }
    #endregion

}
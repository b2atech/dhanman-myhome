using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.TicketStatuses;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.TicketStatuses;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetTicketStatusesQueryHandler : IQueryHandler<GetTicketStatusesQuery, Result<TicketStatusListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetTicketStatusesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<TicketStatusListResponse>> Handle(GetTicketStatusesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var ticketStatuses = await _dbContext.SetInt<TicketStatus>()
                  .AsNoTracking()
                  .Select(e => new TicketStatusResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new TicketStatusListResponse(ticketStatuses);

                  return listResponse;
              });
    }
    #endregion

}
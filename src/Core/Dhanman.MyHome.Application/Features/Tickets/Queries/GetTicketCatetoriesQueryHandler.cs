using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.TicketCatetories;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.TicketCategories;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetTicketCatetoriesQueryHandler : IQueryHandler<GetTicketCatetoriesQuery, Result<TicketCatetoryListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetTicketCatetoriesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<TicketCatetoryListResponse>> Handle(GetTicketCatetoriesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var ticketCategories = await _dbContext.SetInt<TicketCategory>()
                  .AsNoTracking()
                  .Select(e => new TicketCatetoryResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new TicketCatetoryListResponse(ticketCategories);

                  return listResponse;
              });
    }
    #endregion

}
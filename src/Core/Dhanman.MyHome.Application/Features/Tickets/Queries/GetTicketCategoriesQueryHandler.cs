using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.TicketCategories;
using Dhanman.MyHome.Application.Contracts.TicketCatetories;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.TicketCategories;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetTicketCategoriesQueryHandler : IQueryHandler<GetTicketCategoriesQuery, Result<TicketCategoryListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetTicketCategoriesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<TicketCategoryListResponse>> Handle(GetTicketCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var ticketCategories = await _dbContext.SetInt<TicketCategory>()
                  .AsNoTracking()
                  .Select(e => new TicketCategoryResponse(
                          e.Id,
                          e.Name))
                  .ToListAsync(cancellationToken);

                  var listResponse = new TicketCategoryListResponse(ticketCategories);

                  return listResponse;
              });
    }
    #endregion

}
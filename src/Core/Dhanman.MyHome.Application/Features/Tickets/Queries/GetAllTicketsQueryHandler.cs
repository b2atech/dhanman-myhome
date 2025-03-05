using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Tickets;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.TicketCategories;
using Dhanman.MyHome.Domain.Entities.TicketPriorities;
using Dhanman.MyHome.Domain.Entities.Tickets;
using Dhanman.MyHome.Domain.Entities.TicketStatuses;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;

public class GetAllTicketsQueryHandler : IQueryHandler<GetAllTicketsQuery, Result<TicketListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllTicketsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<TicketListResponse>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query.ApartmentId != Guid.Empty, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var tickets = await _dbContext.Set<Ticket>()
                  .AsNoTracking()
                  .Where(x => x.ApartmentId == query.ApartmentId)
                  .Select(e => new TicketResponse(
                          e.Id,
                          e.UnitId,
                          _dbContext.SetInt<Unit>()
                                  .Where(p => p.Id == e.UnitId)
                                  .Select(p => p.Name).FirstOrDefault(),                     
                          e.Title,
                          e.Description,
                          e.TicketCategoryId,
                          _dbContext.SetInt<TicketCategory>()
                                  .Where(p => p.Id == e.TicketCategoryId)
                                  .Select(p => p.Name).FirstOrDefault(),                       
                          e.TicketPriorityId,
                          _dbContext.SetInt<TicketPriority>()
                                  .Where(p => p.Id == e.TicketPriorityId)
                                  .Select(p => p.Name).FirstOrDefault(),                          
                          e.TicketStatusId,
                          _dbContext.SetInt<TicketStatus>()
                                  .Where(p => p.Id == e.TicketStatusId)
                                  .Select(p => p.Name).FirstOrDefault(),                       
                          e.TicketAssignedTo,
                           _dbContext.SetInt<Visitor>()
                                  .Where(p => p.Id == e.TicketAssignedTo)
                                  .Select(p => p.FirstName + " " + p.LastName).FirstOrDefault(),                 
                          e.CreatedBy,
                          e.CreatedOnUtc,
                          e.ModifiedBy,
                          e.ModifiedOnUtc))
                  .ToListAsync(cancellationToken);

                  var listResponse = new TicketListResponse(tickets);

                  return listResponse;
              });
    }
    #endregion

}
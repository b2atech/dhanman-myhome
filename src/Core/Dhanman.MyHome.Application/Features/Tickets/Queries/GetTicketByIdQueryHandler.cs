using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Tickets;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using Dhanman.MyHome.Domain.Entities.TicketCategories;
using Dhanman.MyHome.Domain.Entities.TicketPriorities;
using Dhanman.MyHome.Domain.Entities.Tickets;
using Dhanman.MyHome.Domain.Entities.TicketStatuses;
using Dhanman.MyHome.Domain.Entities.Units;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Tickets.Queries;
 
public class GetTicketByIdQueryHandler : IQueryHandler<GetTicketByIdQuery, Result<TicketResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetTicketByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<TicketResponse>> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken) =>

        await Result.Success(request)
           .Ensure(query => query.TicketId != Guid.Empty, Errors.General.EntityNotFound)
           .Bind(query =>
               _dbContext.Set<Ticket>().AsNoTracking()
                   .Where(e => e.Id == request.TicketId)
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
                           _dbContext.SetInt<ServiceProvider>()
                                  .Where(p => p.Id == e.TicketAssignedTo)
                                  .Select(p => p.FirstName + " " + p.LastName).FirstOrDefault(),
                          e.CreatedBy,
                          e.CreatedOnUtc,
                          e.ModifiedBy,
                          e.ModifiedOnUtc
                       ))
                   .FirstOrDefaultAsync(cancellationToken))
           .Ensure(response => response != null, Errors.General.EntityNotFound);     
    #endregion

}
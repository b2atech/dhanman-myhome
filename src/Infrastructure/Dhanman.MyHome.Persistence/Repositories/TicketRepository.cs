using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Tickets;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Dhanman.MyHome.Persistence.Repositories;

public sealed class TicketRepository : ITicketRepository
{

    #region Properties
    private readonly IApplicationDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructor
    public TicketRepository(IApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _dbContext = dbContext;
    }
    #endregion

    #region Methods
    public void Insert(Ticket ticket) => _dbContext.Insert(ticket);

    public void Update(Ticket ticket) => _dbContext.Insert(ticket);

    public void Delete(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task<Ticket?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task<Ticket?> ITicketRepository.GetByIdAsync(Guid id) => _dbContext.GetBydIdAsync<Ticket>(id);

    public async Task<List<Ticket>> UpdateStatus(Guid apartmentId, int ticketStatusId, List<Guid> ticketIds, Guid modifiedBy, CancellationToken cancellationToken)
    {
        await _dbContext.Database.ExecuteSqlRawAsync(
           "CALL public.update_ticket_status(@p_apartment_id,@p_ticket_status_id, @p_ticket_ids, @p_modified_by)",
           new NpgsqlParameter("p_apartment_id", apartmentId),
           new NpgsqlParameter("p_ticket_status_id", ticketStatusId),
           new NpgsqlParameter("p_ticket_ids", ticketIds.ToArray()),
           new NpgsqlParameter("p_modified_by", modifiedBy)
        );

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var tickets = await _dbContext.Set<Ticket>()
        .Where(x => ticketIds.Contains(x.Id))
        .ToListAsync();

        return tickets.Where(x => x.TicketStatusId == ticketStatusId).ToList();
    }

    #endregion
}

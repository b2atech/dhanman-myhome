using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Tickets;

namespace Dhanman.MyHome.Persistence.Repositories;

public sealed class TicketRepository : ITicketRepository
{

    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public TicketRepository(IApplicationDbContext dbContext) => _dbContext = dbContext;

    

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

    #endregion
}

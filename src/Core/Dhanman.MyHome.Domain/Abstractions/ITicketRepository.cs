using Dhanman.MyHome.Domain.Entities.Floors;
using Dhanman.MyHome.Domain.Entities.Tickets;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface ITicketRepository
{
    #region Methods

    Task<Ticket?> GetByIdAsync(Guid id);
    void Insert(Ticket ticket);
    void Update(Ticket ticket);
    void Delete(Ticket ticket);

    #endregion
}

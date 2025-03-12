using Dhanman.MyHome.Domain.Entities.Tickets;

namespace Dhanman.MyHome.Domain.Abstractions;

public interface ITicketRepository
{
    #region Methods

    Task<Ticket?> GetByIdAsync(Guid id);
    void Insert(Ticket ticket);
    void Update(Ticket ticket);
    Task<List<Ticket>> UpdateStatus(Guid apartmentId, int ticketStatusId, List<Guid> ticketIds, Guid modifiedBy, CancellationToken cancellationToken);

    void Delete(Ticket ticket);

    #endregion
}

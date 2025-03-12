using Dhanman.MyHome.Domain.Exceptions.Base;

namespace Dhanman.MyHome.Domain.Exceptions;

public class TicketNotFoundException : NotFoundException
{
    #region Constructor
    public TicketNotFoundException(Guid ticketId)
        : base($"The ticket id with the identifier {ticketId} was not found.")
    {
    }
    #endregion
}

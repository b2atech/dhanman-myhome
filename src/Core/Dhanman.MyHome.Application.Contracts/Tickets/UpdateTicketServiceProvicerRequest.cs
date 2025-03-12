namespace Dhanman.MyHome.Application.Contracts.Tickets;

public class UpdateTicketServiceProvicerRequest
{
    #region Properties
    public Guid TicketId { get; set; }
    public int ServiceProviderId { get; set; }
    #endregion

    #region Constructor
    public UpdateTicketServiceProvicerRequest() { }
    #endregion
}

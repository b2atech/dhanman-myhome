namespace Dhanman.MyHome.Application.Contracts.TicketStatuses;

public class UpdateTicketStatusRequest
{
    #region Properties
    public List<Guid> TicketIds { get; set; }
    public Guid ApartmentId { get; set; }
    #endregion

    #region Constructor
    public UpdateTicketStatusRequest() { }
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.Tickets;

public class CreateTicketRequest
{
    #region Properties
    public Guid Id { get; set; }
    public int UnitId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TicketCategoryId { get; set; }
    public int TicketPriorityId { get; set; }
    public int TicketStatusId { get; set; }
    public int? TicketAssignedTo { get; private set; }
    #endregion

    #region Constructor
    public CreateTicketRequest()
    {

    }
    #endregion
}

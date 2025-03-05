namespace Dhanman.MyHome.Application.Contracts.Tickets;

public sealed class TicketResponse
{
    #region Properties
    public Guid Id { get; }
    public int UnitId { get; }
    public string Unit { get; }
    public string Title { get; }
    public string Description { get; }
    public int TicketCategoryId { get; }
    public string TicketCategory { get; }
    public int TicketPriorityId { get; }
    public string TicketPriority { get; }
    public int TicketStatusId { get; }
    public string TicketStatus { get; }
    public int? TicketAssignedTo { get; }
    public string TicketAssigned { get; }
    public Guid CreatedBy { get; }
    public DateTime CreatedOnUtc { get; }
    public Guid? ModifiedBy { get; }
    public DateTime? ModifiedOnUtc { get; }
    #endregion

    #region Constructor
    public TicketResponse(Guid id, int unitId, string unit, string title, string description, int ticketCategoryId, string ticketCategory, int ticketPriorityId, string ticketPriority, int ticketStatusId, string ticketStatus, int? ticketAssignedTo, string ticketAssigned, Guid createdBy, DateTime createdOnUtc, Guid? modifiedBy, DateTime? modifiedOnUtc)
    {
        Id = id;
        UnitId = unitId;
        Unit = unit;
        Title = title;
        Description = description;
        TicketCategoryId = ticketCategoryId;
        TicketCategory = ticketCategory;
        TicketPriorityId = ticketPriorityId;
        TicketPriority = ticketPriority;
        TicketStatusId = ticketStatusId;
        TicketStatus = ticketStatus;
        TicketAssignedTo = ticketAssignedTo;
        TicketAssigned = ticketAssigned;
        CreatedBy = createdBy;
        CreatedOnUtc = createdOnUtc;
        ModifiedBy = modifiedBy;
        ModifiedOnUtc = modifiedOnUtc;
    }

    #endregion
}
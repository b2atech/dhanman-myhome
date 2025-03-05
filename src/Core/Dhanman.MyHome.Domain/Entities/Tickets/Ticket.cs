using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Tickets;

public class Ticket : Entity, IAuditableEntity, ISoftDeletableEntity
{

    #region Properties
    public Guid ApartmentId { get; set; }
    public int UnitId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TicketCategoryId { get; set; }
    public int TicketPriorityId { get; set; }
    public int TicketStatusId { get; set; }
    public int? TicketAssignedTo { get; private set; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; protected set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    #endregion

    #region Constructors
    public Ticket() { }

    public Ticket(Guid id, Guid apartmentId, int unitId, string title, string description, int ticketCategoryId, int ticketPriorityId, int ticketStatusId, int? ticketAssignedTo)
    {
        Id = id;
        ApartmentId = apartmentId;
        UnitId = unitId;
        Title = title;
        Description = description;
        TicketCategoryId = ticketCategoryId;
        TicketPriorityId = ticketPriorityId;
        TicketStatusId = ticketStatusId;
        TicketAssignedTo = ticketAssignedTo;
    }
    #endregion
}

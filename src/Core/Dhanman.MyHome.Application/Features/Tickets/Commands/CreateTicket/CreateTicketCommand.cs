using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.Tickets.Commands.CreateTicket;

public class CreateTicketCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public Guid Id { get; set; }
    public Guid ApartmentId { get; set; }
    public int UnitId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int TicketCategoryId { get; set; }
    public int TicketPriorityId { get; set; }
    public int TicketStatusId { get; set; }
    public int? TicketAssignedTo { get; set; }
    #endregion

    #region Constructor
    public CreateTicketCommand() { }

    public CreateTicketCommand(Guid id, Guid apartmentId, int unitId, string title, string description, int ticketCategoryId, int ticketPriorityId, int ticketStatusId, int? ticketAssignedTo)
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

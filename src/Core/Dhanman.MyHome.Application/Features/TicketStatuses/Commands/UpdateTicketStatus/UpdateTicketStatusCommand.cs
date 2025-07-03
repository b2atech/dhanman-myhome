using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.TicketStatuses.Commands.UpdateTicketNextStatus;

public class UpdateTicketStatusCommand : ICommand<Result<EntityUpdatedResponse>>
{
    public List<Guid> TicketIds { get; set; }
    public Guid ApartmentId { get; set; }
    public int TicketStatusId { get; set; }
    public Guid CreatedBy { get; set; }

    public UpdateTicketStatusCommand(List<Guid> ticketIds, Guid apartmentId, int ticketStatusId, Guid createdBy)
    {
        TicketIds = ticketIds;
        ApartmentId = apartmentId;
        TicketStatusId = ticketStatusId;
        CreatedBy = createdBy;
    }
}

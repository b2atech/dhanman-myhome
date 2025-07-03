using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.Tickets.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Tickets;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Tickets.Commands.CreateTicket;

public class CreateTicketCommandHandler : ICommandHandler<CreateTicketCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly ITicketRepository _ticketRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateTicketCommandHandler(IMediator mediator, ITicketRepository ticketRepository)
    {
        _mediator = mediator;
        _ticketRepository = ticketRepository;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticketRequest = new Ticket(
            request.Id,
            request.ApartmentId,
            request.UnitId,
            request.Title,
            request.Description,
            request.TicketCategoryId,
            request.TicketPriorityId, 
            request.TicketStatusId,
            request.TicketAssignedTo
            );

        _ticketRepository.Insert(ticketRequest);
        await _mediator.Publish(new TicketCreatedEvent(ticketRequest.Id), cancellationToken);
        return Result<EntityCreatedResponse>.Success(new EntityCreatedResponse(ticketRequest.Id));
    }
    #endregion
}

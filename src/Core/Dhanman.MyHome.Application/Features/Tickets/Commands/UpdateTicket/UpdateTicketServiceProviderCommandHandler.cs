using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.Tickets.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Tickets.Commands.UpdateTicket;

public class UpdateTicketServiceProviderCommandHandler : ICommandHandler<UpdateTicketServiceProviderCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly ITicketRepository _ticketRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructor
    public UpdateTicketServiceProviderCommandHandler(ITicketRepository ticketRepository, IMediator mediator)
    {
        _ticketRepository = ticketRepository;
        _mediator = mediator;
    }

    #endregion

    #region Methods
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateTicketServiceProviderCommand request, CancellationToken cancellationToken)
    {
        var updateticket = await _ticketRepository.GetByIdAsync(request.TicketId);

        if (updateticket == null)
        {
            throw new TicketNotFoundException(request.TicketId);
        }
        updateticket.TicketAssignedTo = request.ServiceProviderId > 0 ? request.ServiceProviderId : updateticket.TicketAssignedTo;

        _ticketRepository.Update(updateticket);

        await _mediator.Publish(new ServiceProviderTicketUpdateEvent(updateticket.Id), cancellationToken);

        return Result.Success(new EntityUpdatedResponse(updateticket.Id));

    }
    #endregion
}

using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Domain.Abstractions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.TicketStatuses.Commands.UpdateTicketNextStatus;

public class UpdateTicketStatusCommandHandler : ICommandHandler<UpdateTicketStatusCommand, Result<EntityUpdatedResponse>>
{
    private readonly ITicketRepository _ticketRepository;

    public UpdateTicketStatusCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
    {
        await _ticketRepository.UpdateStatus(request.ApartmentId, request.TicketStatusId, request.TicketIds, request.CreatedBy, cancellationToken);

        var firstTicketId = request.TicketIds.FirstOrDefault();
        return Result.Success(new EntityUpdatedResponse(firstTicketId));
    }
}
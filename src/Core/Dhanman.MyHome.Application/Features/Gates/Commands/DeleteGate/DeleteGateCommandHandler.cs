using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.Gates.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Gates.Commands.DeleteGate;

public class DeleteGateCommandHandler : ICommandHandler<DeleteGateCommand, Result<EntityDeletedResponse>>
{
    #region Properties
    private readonly IGateRepository _gateRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public DeleteGateCommandHandler(IGateRepository gateRepository, IMediator mediator)
    {
        _gateRepository = gateRepository;
        _mediator = mediator;
    }
    #endregion
    public async Task<Result<EntityDeletedResponse>> Handle(DeleteGateCommand request, CancellationToken cancellationToken)
    {
        var gate = await _gateRepository.GetByIntIdAsync(request.GateId);

        if (gate == null)
        {
            throw new GateNotFoundException(request.GateId);
        }

        gate.IsDeleted = true;

        _gateRepository.Update(gate);

        await _mediator.Publish(new GateDeletedEvent(gate.Id), cancellationToken);

        return Result.Success(new EntityDeletedResponse(gate.Id));
    }
}

using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Gates.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Gates;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Gates.Commands.CreateGate;

public class CreateGateCommandHandler : ICommandHandler<CreateGateCommand, Result<EntityCreatedResponse>>
{

    #region Properties
    private readonly IGateRepository _gateRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateGateCommandHandler(IGateRepository gateRepository, IMediator mediator)
    {
        _gateRepository = gateRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateGateCommand request, CancellationToken cancellationToken)
    {
        int lastId = await _gateRepository.GetLastGateIdAsync();
        int newId = lastId + 1;

        var gate = new Gate(
            newId,
            request.Name,
            request.ApartmentId,
            request.BuildingId,
            request.GateTypeId,
            request.IsUsedForIn,
            request.IsUsedForOut,
            request.IsAllUsersAllowed,
            request.IsResidentsAllowed,
            request.IsStaffAllowed,
            request.IsVendorAllowed
        );

        _gateRepository.Insert(gate);
        await _mediator.Publish(new GateCreatedEvent(gate.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(gate.Id));
    }
    #endregion
}

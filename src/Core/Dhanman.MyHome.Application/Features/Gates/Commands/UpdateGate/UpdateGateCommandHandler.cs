using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Gates.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Gates.Commands.UpdateGate;

public class UpdateGateCommandHandler : ICommandHandler<UpdateGateCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IGateRepository _gateRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public UpdateGateCommandHandler(IGateRepository gateRepository, IMediator mediator)
    {
        _gateRepository = gateRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateGateCommand request, CancellationToken cancellationToken)
    {
       var  gate = await _gateRepository.GetByIntIdAsync(request.GateId);

        if(gate == null)
        {
            throw new GateNotFoundException(request.GateId);
        }

        gate.Name = request.Name ?? gate.Name;

        gate.ApartmentId = request.ApartmentId != Guid.Empty ? request.ApartmentId : gate.ApartmentId;

        gate.BuildingId = request.BuildingId > 0 ? request.BuildingId : gate.BuildingId;

        gate.GateTypeId = request.GateTypeId > 0 ? request.GateTypeId : gate.GateTypeId;

        gate.IsUsedForIn = request.IsUsedForIn ? request.IsUsedForIn : gate.IsUsedForIn;

        gate.IsUsedForOut = request.IsUsedForOut ? request.IsUsedForOut : gate.IsUsedForOut;

        gate.IsAllUsersAllowed = request.IsAllUsersAllowed ? request.IsAllUsersAllowed : gate.IsAllUsersAllowed;

        gate.IsResidentsAllowed = request.IsResidentsAllowed ? request.IsResidentsAllowed : gate.IsResidentsAllowed;

        gate.IsStaffAllowed = request.IsStaffAllowed ? request.IsStaffAllowed: gate.IsStaffAllowed;

        gate.IsVendorAllowed = request.IsVendorAllowed ? request.IsVendorAllowed : gate.IsVendorAllowed;


        _gateRepository.Update(gate);
        await _mediator.Publish(new GateCreatedEvent(gate.Id), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(gate.Id));
    }
    #endregion
}

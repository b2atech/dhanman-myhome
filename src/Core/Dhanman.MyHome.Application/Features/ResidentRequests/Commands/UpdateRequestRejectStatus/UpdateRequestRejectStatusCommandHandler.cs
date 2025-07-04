using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.Shared.Contracts.Common;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Commands.UpdateRequestRejectStatus;

public class UpdateRequestRejectStatusCommandHandler : ICommandHandler<UpdateRequestRejectStatusCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IResidentRequestRepository _residentRequestRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public UpdateRequestRejectStatusCommandHandler(IResidentRequestRepository residentRequestRepository, IMediator mediator)
    {
        _residentRequestRepository = residentRequestRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateRequestRejectStatusCommand request, CancellationToken cancellationToken)
    {
        var updateRequestRejectStatus = await _residentRequestRepository.GetBydIdIntAsync(request.Id);

        if (updateRequestRejectStatus is null)
        {
            throw new RequestIdNotFoundException(request.Id);
        }

        updateRequestRejectStatus.RequestStatusId = ResidentRequestStatus.REJECTED;

        _residentRequestRepository.Update(updateRequestRejectStatus);

        await _mediator.Publish(new ResidentRequestUpdatedEvent(updateRequestRejectStatus.Id), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(updateRequestRejectStatus.Id));
    }

    #endregion

}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Commands.UpdateRequestApproveStatus;

public class UpdateRequestApproveStatusCommandHandler : ICommandHandler<UpdateRequestApproveStatusCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IResidentRequestRepository _residentRequestRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public UpdateRequestApproveStatusCommandHandler(IResidentRequestRepository residentRequestRepository, IMediator mediator)
    {
        _residentRequestRepository = residentRequestRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateRequestApproveStatusCommand request, CancellationToken cancellationToken)
    {
        var updateRequestApproveStatus = await _residentRequestRepository.GetBydIdIntAsync(request.Id);

        if (updateRequestApproveStatus is null)
        {
            throw new RequestIdNotFoundException(request.Id);
        }
        
        updateRequestApproveStatus.RequestStatusId = ResidentRequestStatus.APPROVED;

        _residentRequestRepository.Update(updateRequestApproveStatus);

        await _mediator.Publish(new ResidentRequestUpdatedEvent(updateRequestApproveStatus.Id), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(updateRequestApproveStatus.Id));
    }

    #endregion

}
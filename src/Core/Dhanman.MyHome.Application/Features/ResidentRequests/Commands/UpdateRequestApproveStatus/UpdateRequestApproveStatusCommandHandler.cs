using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;
using ResidentRequestStatus = Dhanman.MyHome.Application.Constants.ResidentRequestStatus;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Commands.UpdateRequestApproveStatus;

public class UpdateRequestApproveStatusCommandHandler : ICommandHandler<UpdateRequestApproveStatusCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IResidentRequestRepository _residentRequestRepository;
    private readonly IResidentRepository _residentRepository;
    private readonly IResidentUnitRepository _residentUnitRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructors
    public UpdateRequestApproveStatusCommandHandler(IResidentRequestRepository residentRequestRepository, IResidentRepository residentRepository, IResidentUnitRepository residentUnitRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _residentRequestRepository = residentRequestRepository;
        _residentRepository = residentRepository;
        _residentUnitRepository = residentUnitRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
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

        //int lastResidentId = await _residentRepository.GetLastResidentIdAsync();
       // int newResidentId = lastResidentId + 1;
        var resident = new Resident(updateRequestApproveStatus.ApartmentId, updateRequestApproveStatus.FirstName, updateRequestApproveStatus.LastName, updateRequestApproveStatus.Email, updateRequestApproveStatus.ContactNumber, updateRequestApproveStatus.PermanentAddressId, updateRequestApproveStatus.ResidentTypeId, updateRequestApproveStatus.OccupancyStatusId);
        _residentRepository.Insert(resident);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //int lastResidentUnitId = await _residentUnitRepository.GetLastResidentIdAsync();
       // int newResidentUnitId = lastResidentUnitId + 1;

        var residentUnit = new ResidentUnit( updateRequestApproveStatus.UnitId, resident.Id);
        _residentUnitRepository.Insert(residentUnit);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new ResidentRequestUpdatedEvent(updateRequestApproveStatus.Id), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(updateRequestApproveStatus.Id));
    }

    #endregion

}
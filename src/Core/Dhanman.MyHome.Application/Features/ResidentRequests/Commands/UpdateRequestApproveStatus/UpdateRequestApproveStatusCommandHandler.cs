using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.Users;
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
    private readonly ICommonServiceClient _commonServiceClient;
    private readonly ISalesServiceClient _salesServiceClient;
    private readonly IPurchaseServiceClient _purchaseServiceClient;
    #endregion

    #region Constructors
    public UpdateRequestApproveStatusCommandHandler(IResidentRequestRepository residentRequestRepository, IResidentRepository residentRepository, IResidentUnitRepository residentUnitRepository, IMediator mediator, IUnitOfWork unitOfWork, ICommonServiceClient commonServiceClient, ISalesServiceClient salesServiceClient, IPurchaseServiceClient purchaseServiceClient)
    {
        _residentRequestRepository = residentRequestRepository;
        _residentRepository = residentRepository;
        _residentUnitRepository = residentUnitRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _commonServiceClient = commonServiceClient;
        _salesServiceClient = salesServiceClient;
        _purchaseServiceClient = purchaseServiceClient;
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

        var user = await _residentRequestRepository.GetBydIdIntAsync(request.Id);

        var existingUserResult = await _commonServiceClient.GetUserByEmailOrPhoneAsync(user.Email, user.ContactNumber);


        Guid newUserId;
        if (existingUserResult != null)
        {
            newUserId = existingUserResult.Id;
        }
        else
        {
            newUserId = Guid.NewGuid();
        }

        //int lastResidentId = await _residentRepository.GetLastResidentIdAsync();
        // int newResidentId = lastResidentId + 1;
        var resident = new Resident(updateRequestApproveStatus.ApartmentId, updateRequestApproveStatus.FirstName, updateRequestApproveStatus.LastName, updateRequestApproveStatus.Email, updateRequestApproveStatus.ContactNumber, updateRequestApproveStatus.PermanentAddressId, newUserId, updateRequestApproveStatus.ResidentTypeId, updateRequestApproveStatus.OccupancyStatusId);
        _residentRepository.Insert(resident);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //int lastResidentUnitId = await _residentUnitRepository.GetLastResidentIdAsync();
       // int newResidentUnitId = lastResidentUnitId + 1;

        var residentUnit = new ResidentUnit( updateRequestApproveStatus.UnitId, resident.Id);
        _residentUnitRepository.Insert(residentUnit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var firstName = new Domain.Entities.Users.FirstName(user.FirstName);
        var lastName = new Domain.Entities.Users.LastName(user.LastName);
        var email = new Domain.Entities.Users.Email(user.Email);
        var contactNumber = new ContactNumber(user.ContactNumber);

        var userCopy = new UserDto(newUserId, user.ApartmentId, firstName, lastName, email, contactNumber);

        await _commonServiceClient.CreateUserAsync(userCopy);
        await _salesServiceClient.CreateUserAsync(userCopy);
        await _purchaseServiceClient.CreateUserAsync(userCopy);


        await _mediator.Publish(new ResidentRequestUpdatedEvent(updateRequestApproveStatus.Id), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(updateRequestApproveStatus.Id));
    }

    #endregion

}
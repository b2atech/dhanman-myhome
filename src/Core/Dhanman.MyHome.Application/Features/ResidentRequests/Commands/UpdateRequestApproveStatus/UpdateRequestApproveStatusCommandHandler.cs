using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using B2aTech.CrossCuttingConcern.Messaging;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Abstractions;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Models;
using B2aTech.CrossCuttingConcern.Messaging.RabbitMQ.Services;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Contracts;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.Users;
using Dhanman.MyHome.Domain.Exceptions;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Commands;
using Dhanman.Shared.Contracts.Common;
using Dhanman.Shared.Contracts.Events;
using Dhanman.Shared.Contracts.Routing;
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
    private readonly IEventPublisher _eventPublisher;
    private readonly ICommandPublisher _commandPublisher;
    private readonly IUserContextService _userContextService;
    #endregion

    #region Constructors
    public UpdateRequestApproveStatusCommandHandler(IResidentRequestRepository residentRequestRepository, IResidentRepository residentRepository, IResidentUnitRepository residentUnitRepository, IMediator mediator, IUnitOfWork unitOfWork, ICommonServiceClient commonServiceClient, ISalesServiceClient salesServiceClient, IPurchaseServiceClient purchaseServiceClient, IEventPublisher eventPublisher, ICommandPublisher commandPublisher, IUserContextService userContextService)
    {
        _residentRequestRepository = residentRequestRepository;
        _residentRepository = residentRepository;
        _residentUnitRepository = residentUnitRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _commonServiceClient = commonServiceClient;
        _salesServiceClient = salesServiceClient;
        _purchaseServiceClient = purchaseServiceClient;
        _eventPublisher = eventPublisher;
        _commandPublisher = commandPublisher;
        _userContextService = userContextService;
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

        //var userCopy = new UserDto(newUserId, user.ApartmentId, firstName, lastName, email, contactNumber);

        MessageContext messageContext = new MessageContext
        {
            UserId = _userContextService.CurrentUserId,
            CorrelationId = _userContextService.CorrelationId,
            OrganizationId = _userContextService.OrganizationId,

        };

        //var user = new UserDto(newUserId, request.ApartmentId, firstName, lastName, email, contactNumber);
        var userResident = new CreateUserCommand(newUserId, user.ApartmentId, firstName, lastName, email, contactNumber, messageContext);

        var commandCustomerEnevelop = new CommandEnvelope<CreateUserCommand>
        {
            CommandType = RoutingKeys.Community.CreateCustomerinSalesAfterMember,
            Source = "CommunityService",
            UserId = messageContext.UserId,
            OrganizationId = messageContext.OrganizationId,
            CorrelationId = messageContext.CorrelationId,
            Payload = userResident
        };

        await _commandPublisher.PublishAsync(RoutingKeys.Community.CreateCustomerinSalesAfterMember, commandCustomerEnevelop);

        var commandUser = new CreateUserCommand(newUserId, user.ApartmentId, firstName, lastName, email, contactNumber, messageContext);

        //
        var commandUserEnevlop = new CommandEnvelope<CreateUserCommand>()
        {
            CommandType = RoutingKeys.Community.CreateUserinCommonAfterMember,
            Source = "CommunityService",
            UserId = messageContext.UserId,
            CorrelationId = messageContext.CorrelationId,
            OrganizationId = messageContext.OrganizationId,
            Payload = commandUser,
        };
        await _commandPublisher.PublishAsync(RoutingKeys.Community.CreateUserinCommonAfterMember, commandUserEnevlop);

        await _mediator.Publish(new ResidentRequestUpdatedEvent(updateRequestApproveStatus.Id), cancellationToken);
        return Result.Success(new EntityUpdatedResponse(updateRequestApproveStatus.Id));
    }

    #endregion

}
using B2aTech.CrossCuttingConcern.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Constants.Enums;
using Dhanman.MyHome.Application.Contracts;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.MemberRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Domain.Entities.MemberAdditionalDetails;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.Users;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Unit = Dhanman.MyHome.Domain.Entities.Units.Unit;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Commands.UpdateMemberApproveStatus;

public class UpdateMemberApproveStatusCommandHandler : ICommandHandler<UpdateMemberApproveStatusCommand, Result<EntityUpdatedResponse>>
{
    #region Properties
    private readonly IUnitRepository _unitRepository;
    private readonly ICommonServiceClient _commonServiceClient;
    private readonly ISalesServiceClient _salesServiceClient;
    private readonly IPurchaseServiceClient _purchaseServiceClient;
    private readonly IResidentRequestRepository _residentRequestRepository;
    private readonly IResidentRepository _residentRepository;
    private readonly IResidentUnitRepository _residentUnitRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMemberAdditionalDetailRepository _memberAdditionalDetailRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContextService _userContextService;
    private readonly IApplicationDbContext _dbContext;
    private readonly IEmailService _emailService;
    private readonly ILogger<UpdateMemberApproveStatusCommandHandler> _logger;
    #endregion

    #region Constructors
    public UpdateMemberApproveStatusCommandHandler(IUnitRepository unitRepository, ICommonServiceClient commonServiceClient, ISalesServiceClient salesServiceClient, IPurchaseServiceClient purchaseServiceClient,
                                                   IResidentRequestRepository residentRequestRepository, IResidentRepository residentRepository, IResidentUnitRepository residentUnitRepository,
                                                   IUserRepository userRepository, IMemberAdditionalDetailRepository memberAdditionalDetailRepository, IMediator mediator, IUserContextService userContextService,
                                                   IApplicationDbContext dbContext, IEmailService emailService, ILogger<UpdateMemberApproveStatusCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _unitRepository = unitRepository;
        _commonServiceClient = commonServiceClient;
        _salesServiceClient = salesServiceClient;
        _purchaseServiceClient = purchaseServiceClient;
        _residentRequestRepository = residentRequestRepository;
        _residentRepository = residentRepository;
        _residentUnitRepository = residentUnitRepository;
        _userRepository = userRepository;
        _memberAdditionalDetailRepository = memberAdditionalDetailRepository;
        _mediator = mediator;
        _userContextService = userContextService;
        _dbContext = dbContext;
        _emailService = emailService;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateMemberApproveStatusCommand request, CancellationToken cancellationToken)
    {
        var residentRequest = await _residentRequestRepository.GetBydIdIntAsync(request.Id);

        if (residentRequest == null)
        {
            throw new RequestIdNotFoundException(request.Id);
        }

        string apartmentName = await _dbContext.Set<Apartment>()
                              .Where(p => p.Id == residentRequest.ApartmentId)
                              .Select(p => p.Name).FirstOrDefaultAsync();

        string unitName = $"{residentRequest.FirstName ?? string.Empty} {residentRequest.LastName ?? string.Empty}".Trim();

        var unit = new Unit(unitName, 117, 193, 1, 1, 1, 1, 1, 1, 1, "1.0", "1.1", residentRequest.ApartmentId, Guid.NewGuid());
        _unitRepository.Insert(unit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var customerDto = new Contracts.CustomerDto
        {
            Id = unit.CustomerId,
            Name = unitName,
            CompanyId = residentRequest.ApartmentId,
            CreatedBy = _userContextService.GetCurrentUserId()
        };

        await _commonServiceClient.CreateCustomerAsync(customerDto);
        await _salesServiceClient.CreateCustomerAsync(customerDto);

        residentRequest.RequestStatusId = ResidentRequestStatus.APPROVED;
        residentRequest.UnitId = unit.Id;
        _residentRequestRepository.Update(residentRequest);


        var existingUserResult = await _commonServiceClient.GetUserByEmailOrPhoneAsync(residentRequest.Email, residentRequest.ContactNumber);

        Guid newUserId;
        if (existingUserResult != null)
        {
            newUserId = existingUserResult.Id;
        }
        else
        {
            newUserId = Guid.NewGuid();
        }



        var resident = new Resident(residentRequest.ApartmentId, residentRequest.FirstName, residentRequest.LastName, residentRequest.Email, residentRequest.ContactNumber, residentRequest.PermanentAddressId, newUserId, residentRequest.ResidentTypeId, residentRequest.OccupancyStatusId);
        _residentRepository.Insert(resident);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var residentUnit = new ResidentUnit(unit.Id, resident.Id);
        _residentUnitRepository.Insert(residentUnit);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var user = new User(newUserId, residentRequest.ApartmentId, new Domain.Entities.Users.FirstName(residentRequest.FirstName), new LastName(residentRequest.LastName), new Email(residentRequest.Email), new ContactNumber(residentRequest.ContactNumber), residentRequest.ResidentTypeId == (int)ResidentType.OWNER);
        _userRepository.Insert(user);

        var userDto = new UserDto(newUserId, residentRequest.ApartmentId, user.FirstName, user.LastName, user.Email, user.ContactNumber);

        await _commonServiceClient.CreateUserAsync(userDto);
        await _salesServiceClient.CreateUserAsync(userDto);
        await _purchaseServiceClient.CreateUserAsync(userDto);

        var memberDetail = await _dbContext.Set<MemberAdditionalDetail>().FirstOrDefaultAsync(m => m.Id == residentRequest.MemberAdditionalDetailsId);

        if (memberDetail != null)
        {
            memberDetail.ResidentId = resident.Id;
            _memberAdditionalDetailRepository.Update(memberDetail);
        }

        await _mediator.Publish(new MemberRequestUpdatedEvent(residentRequest.Id), cancellationToken);

        // Send welcome email to the new resident
        if (!string.IsNullOrWhiteSpace(residentRequest.Email))
        {
            var subject = $@"Welcome to {apartmentName}!";
            var body = $@"
        <p>Dear {residentRequest.FirstName} {residentRequest.LastName},</p>
        <p>Welcome to the <strong>{apartmentName}</strong> community! We're excited to have you here.</p>
        <p>Your journey with us begins now, and we're here to ensure you have a seamless and enjoyable experience. You can now log in to your personalized dashboard to manage all your activities, track updates, and stay connected with your community.</p>
        <p>If you have any questions, require assistance, or simply need more information, our dedicated team is always here for you. Please feel free to reach out at any time, and we'll be happy to help!</p>
        <p>We encourage you to explore our features and make the most of everything we offer.</p>
        <p>Warm regards,<br/>The {apartmentName} Team</p>
    ";

            var emailResult = await _emailService.SendEmailAsync(residentRequest.Email, subject, body, isHtml: true);

            if (!emailResult.IsSuccess)
            {
                _logger.LogError("Failed to send welcome email to {Email}. Reason: {Reason}", residentRequest.Email, emailResult.Error?.Message ?? "Unknown error");
            }
        }



        return Result.Success(new EntityUpdatedResponse(residentRequest.Id));
    }
    #endregion


}
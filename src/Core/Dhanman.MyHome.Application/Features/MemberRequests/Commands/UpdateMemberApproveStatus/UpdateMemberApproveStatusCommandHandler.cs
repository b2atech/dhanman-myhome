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
using Dhanman.MyHome.Domain.Entities.MemberAdditionalDetails;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.Users;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static Dhanman.MyHome.Domain.Errors;
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
    private readonly IUserContextService _userContextService;
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<UpdateMemberApproveStatusCommandHandler> _logger;
    private readonly ISMSService _smsService; 
    #endregion

    #region Constructors
    public UpdateMemberApproveStatusCommandHandler(IUnitRepository unitRepository, ICommonServiceClient commonServiceClient, ISalesServiceClient salesServiceClient, IPurchaseServiceClient purchaseServiceClient,
                                                   IResidentRequestRepository residentRequestRepository, IResidentRepository residentRepository, IResidentUnitRepository residentUnitRepository,
                                                   IUserRepository userRepository, IMemberAdditionalDetailRepository memberAdditionalDetailRepository, IMediator mediator, IUserContextService userContextService,
                                                   IApplicationDbContext dbContext, ISMSService smsService, ILogger<UpdateMemberApproveStatusCommandHandler> logger)
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
        _smsService = smsService;
        _smsService = smsService;
        _logger = logger;
        //_emailService = emailService;
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

        int lastUnitId = await _unitRepository.GetLastUnitIdAsync();
        int newUnitId = lastUnitId + 1;

        string unitName = $"{residentRequest.FirstName ?? string.Empty} {residentRequest.LastName ?? string.Empty}".Trim();

        var unit = new Unit(newUnitId, unitName, 117, 193, 1, 1, 1, 1, 1, 1, 1, "1.0", "1.1", residentRequest.ApartmentId, Guid.NewGuid());
        _unitRepository.Insert(unit);

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

        int newResidentId = _residentRepository.GetTotalRecordsCount() + 1;
        Guid newUserId = Guid.NewGuid();

        var resident = new Resident(newResidentId, residentRequest.ApartmentId, residentRequest.FirstName, residentRequest.LastName, residentRequest.Email, residentRequest.ContactNumber, residentRequest.PermanentAddressId, newUserId, residentRequest.ResidentTypeId, residentRequest.OccupancyStatusId);
        _residentRepository.Insert(resident);

        int newResidentUnitId = _residentUnitRepository.GetTotalRecordsCount() + 1;
        var residentUnit = new ResidentUnit(newResidentUnitId, unit.Id, newResidentId);
        _residentUnitRepository.Insert(residentUnit);

        var user = new User(newUserId, residentRequest.ApartmentId, new Domain.Entities.Users.FirstName(residentRequest.FirstName), new Domain.Entities.Users.LastName(residentRequest.LastName), new Domain.Entities.Users.Email(residentRequest.Email), new ContactNumber(residentRequest.ContactNumber), residentRequest.ResidentTypeId == (int)ResidentType.OWNER);
        _userRepository.Insert(user);

        var userDto = new UserDto(newUserId, residentRequest.ApartmentId, user.FirstName, user.LastName, user.Email, user.ContactNumber);

        await _commonServiceClient.CreateUserAsync(userDto);
        await _salesServiceClient.CreateUserAsync(userDto);
        await _purchaseServiceClient.CreateUserAsync(userDto);

        var memberDetail = await _dbContext.Set<MemberAdditionalDetail>().FirstOrDefaultAsync(m => m.Id == residentRequest.MemberAdditionalDetailsId);

        if (memberDetail != null)
        {
            memberDetail.ResidentId = newResidentId;
            _memberAdditionalDetailRepository.Update(memberDetail);
        }

      //  try
      //  {
            var success = await _smsService.SendSMS(
                recipient: "pranitkhamkar629@gmail.com", // Or use the hardcoded email for testing
                welcomeMessage: $"Hi {residentRequest.FirstName}, your membership has been approved. Welcome to our community!");

            if (success)
            {
                _logger.LogInformation("✅ Welcome email successfully sent to {Email}", "pranitkhamkar629@gmail.com");
            }
            else
            {
                _logger.LogWarning("⚠ Failed to send welcome email to {Email}.", "pranitkhamkar629@gmail.com");
            }

     //   }

        //catch (Exception ex)
        //{
        //    _logger.LogError(ex, "❌ Exception occurred while sending welcome email to {Email}", "pranitkhamkar629@gmail.com");
        //}



        // Send SMS using the recipient's email
        //await _sMSService.SendSMS(recipient.Email, request.Body);

        //await _emailService.SendEmailAsync(
        //to: residentRequest.Email,
        //    subject: "Welcome to the Community!",
        //    body: $"Hi {residentRequest.FirstName},<br/><br/>Your membership has been approved. Welcome to the community!",
        //    isHtml: true);

        await _mediator.Publish(new MemberRequestUpdatedEvent(residentRequest.Id), cancellationToken);

        return Result.Success(new EntityUpdatedResponse(residentRequest.Id));
    }
    #endregion


}
using B2aTech.CrossCuttingConcern.Core.Result;
using B2aTech.CrossCuttingConcern.Services;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.MemberRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;
using Dhanman.MyHome.Domain.Entities.Cities;
using Dhanman.MyHome.Domain.Entities.MemberAdditionalDetails;
using Dhanman.MyHome.Domain.Entities.ResidentRequests;
using MediatR;
using MemberAdditionalDetails = Dhanman.MyHome.Application.Contracts.MemberRequests.MemberAdditionalDetails;
using MemberRequestAddress = Dhanman.MyHome.Application.Contracts.ServiceProviders.Address;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Commands.CreateMemberRequest;

public class CreateMemberRequestCommandHandler : ICommandHandler<CreateMemberRequestCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentRequestRepository _residentRequestRepository;
    private readonly IMemberAdditionalDetailRepository _memberAdditionalDetailRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _dbContext;
    private readonly TemplatedEmailService _templatedEmailService;

    #endregion

    #region Constructors
    public CreateMemberRequestCommandHandler(IResidentRequestRepository residentRequestRepository, IMemberAdditionalDetailRepository memberAdditionalDetailRepository, IAddressRepository addressRepository, IMediator mediator, IApplicationDbContext dbContext , TemplatedEmailService templatedEmailService)
    {
        _residentRequestRepository = residentRequestRepository;
        _memberAdditionalDetailRepository = memberAdditionalDetailRepository;
        _addressRepository = addressRepository;
        _mediator = mediator;
        _dbContext = dbContext;
        _templatedEmailService = templatedEmailService;

    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateMemberRequestCommand request, CancellationToken cancellationToken)
    {
        //Guid cityId = GetCityId(request.CurrentAddress.CityName, request.CurrentAddress.ZipCode, request.CurrentAddress.StateId);
        //var currentAddress = GetAddress(request.CurrentAddress, cityId);
        //_addressRepository.Insert(currentAddress);
        //Guid addressId = currentAddress.Id;
        Guid addressId = Guid.Empty; 

        var memberAdditionalDetails = GetMemberAdditionalDetails(request.MemberAdditionalDetails);
        _memberAdditionalDetailRepository.Insert(memberAdditionalDetails);
        Guid memberAdditionalDetailsId = memberAdditionalDetails.Id;
        
        int nextresidentRequestId = _residentRequestRepository.GetTotalRecordsCount() + 1;
        int requestStatusId = ResidentRequestStatus.PENDING_REQUEST;

        var residentRequest = new ResidentRequest(nextresidentRequestId, request.ApartmentId, request.FirstName, request.LastName, request.Email, request.ContactNumber, addressId, requestStatusId, 1, 1, memberAdditionalDetailsId);

        _residentRequestRepository.Insert(residentRequest);
        await SendMemberRequestConfirmation(request);
        await _mediator.Publish(new MemberRequestCreatedEvent(residentRequest.Id), cancellationToken);
        
        return Result.Success(new EntityCreatedResponse(residentRequest.Id));
    }
    public async Task SendMemberRequestConfirmation(CreateMemberRequestCommand request)
    {
        var placeholders = new Dictionary<string, string>
        {
            ["member_first_name"] = request.FirstName,
            ["member_last_name"] = request.LastName
        };

        await _templatedEmailService.SendAsync(2, request.Email, placeholders);
    }
    private Guid GetCityId(string cityName, string zipCode, Guid stateId)
    {
        string lowerCityName = cityName.ToLower();
        var city = _dbContext.Set<City>().FirstOrDefault(x => x.Name.ToLower() == lowerCityName && x.ZipCode == zipCode);

        if (city == null)
        {
            city = new City(Guid.NewGuid(), stateId, zipCode, cityName);
            _dbContext.Set<City>().Add(city);
        }
        return city.Id;
    }

    private Address GetAddress(MemberRequestAddress address, Guid cityId)
    {
        return new Address(Guid.NewGuid(), address.CountryId, address.StateId, cityId, address.AddressLine1, address.AddressLine2, address.ZipCode);
    }

    private MemberAdditionalDetail GetMemberAdditionalDetails(MemberAdditionalDetails memberAdditionalDetails)
    {
        return new MemberAdditionalDetail(Guid.NewGuid(), memberAdditionalDetails.MemberType, memberAdditionalDetails.UserName, memberAdditionalDetails.Password, memberAdditionalDetails.CompanyName,
                                           memberAdditionalDetails.Designation, memberAdditionalDetails.HattyId, memberAdditionalDetails.DateOfBirth, memberAdditionalDetails.Gender,
                                           memberAdditionalDetails.MaritalStatus, memberAdditionalDetails.AboutYourSelf, memberAdditionalDetails.SpouseName,
                                           memberAdditionalDetails.SpouseHattyId);
    }
    #endregion

}
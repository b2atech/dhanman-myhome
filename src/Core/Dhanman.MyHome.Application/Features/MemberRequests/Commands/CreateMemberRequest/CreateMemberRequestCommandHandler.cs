using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;
using Dhanman.MyHome.Domain.Entities.Cities;
using Dhanman.MyHome.Domain.Entities.MemberRequests;
using Dhanman.MyHome.Domain.Entities.ResidentRequests;
using MediatR;
using MemberRequestAddress = Dhanman.MyHome.Application.Contracts.ServiceProviders.Address;

namespace Dhanman.MyHome.Application.Features.MemberRequests.Commands.CreateMemberRequest;

public class CreateMemberRequestCommandHandler : ICommandHandler<CreateMemberRequestCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IMemberRequestRepository _memberRequestRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CreateMemberRequestCommandHandler(IMemberRequestRepository memberRequestRepository, IAddressRepository addressRepository, IMediator mediator, IApplicationDbContext dbContext)
    {
        _memberRequestRepository = memberRequestRepository;
        _addressRepository = addressRepository;
        _mediator = mediator;
        _dbContext = dbContext;

    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateMemberRequestCommand request, CancellationToken cancellationToken)
    {
        Guid cityId = GetCityId(request.CurrentAddress.CityName, request.CurrentAddress.ZipCode, request.CurrentAddress.StateId);
        var permanentAddress = GetAddress(request.CurrentAddress, cityId);
        _addressRepository.Insert(permanentAddress);
        Guid addressId = permanentAddress.Id;


        int nextresidentRequestId = _memberRequestRepository.GetTotalRecordsCount() + 1;
        int requestStatusId = ResidentRequestStatus.PENDING_REQUEST;

        var memberRequest = new MemberRequest(nextresidentRequestId,
                                                                     request.MemberType,
                                                                     request.UserName,
                                                                     request.Password,
                                                                     request.FirstName,
                                                                     request.LastName,
                                                                     request.HattyId,
                                                                     request.Email,
                                                                     request.MobileNumber,
                                                                     request.CompanyName,
                                                                     request.Designation,
                                                                     addressId,
                                                                     request.DateOfBirth,
                                                                     request.Gender,
                                                                     request.MaritalStatus,
                                                                     request.AboutYourSelf,
                                                                     request.SpouseName,
                                                                     request.SpouseHattyId,
                                                                     requestStatusId);                                                                                

        _memberRequestRepository.Insert(memberRequest);

        await _mediator.Publish(new ResidentRequestCreatedEvent(memberRequest.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(memberRequest.Id));
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
    #endregion

}
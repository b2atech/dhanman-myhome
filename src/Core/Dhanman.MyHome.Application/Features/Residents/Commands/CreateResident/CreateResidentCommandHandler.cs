using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Residents.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;
using Dhanman.MyHome.Domain.Entities.Cities;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.Users;
using Dhanman.MyHome.Application.Constants.Enums;
using MediatR;
using ResidentAddress = Dhanman.MyHome.Application.Contracts.ServiceProviders.Address;
using Dhanman.MyHome.Application.Contracts;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.ServiceClient;

namespace Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;

public class CreateResidentCommandHandler : ICommandHandler<CreateResidentCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentRepository _residentRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IResidentUnitRepository _residentUnitRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISalesServiceClient _salesServiceClient;
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CreateResidentCommandHandler(IResidentRepository residentRepository, IAddressRepository addressRepository, IResidentUnitRepository residentUnitRepository, IUserRepository userRepository, ISalesServiceClient salesServiceClient, IMediator mediator, IApplicationDbContext dbContext)
    {
        _residentRepository = residentRepository;
        _addressRepository = addressRepository;
        _residentUnitRepository = residentUnitRepository;
        _userRepository = userRepository;
        _salesServiceClient = salesServiceClient;
        _mediator = mediator;
        _dbContext = dbContext;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateResidentCommand request, CancellationToken cancellationToken)
    {
        ResidentUnit residentUnit;
        Resident resident = _residentRepository.GetByEmail(request.Email);

        if (resident != null)
        {
            //var assignedUnits = _residentUnitRepository.GetByResidentId(resident.Id);
            residentUnit = new ResidentUnit(request.UnitId, resident.Id);
            _residentUnitRepository.Insert(residentUnit);
        }
        else
        {
            Guid? permanentAddressId = null;
            if (request.PermanentAddress != null)
            {
                Guid cityId = GetCityId(request.PermanentAddress.CityName, request.PermanentAddress.ZipCode, request.PermanentAddress.StateId);

                 var permanentAddress = GetAddress(request.PermanentAddress, cityId);
                _addressRepository.Insert(permanentAddress);
                permanentAddressId = permanentAddress.Id;
            }

            int nextresidentId = _residentRepository.GetTotalRecordsCount() + 1;

            resident = new Resident(nextresidentId, request.ApartmentId, request.FirstName, request.LastName, request.Email, request.ContactNumber, permanentAddressId, request.ResidentTypeId, request.OccupancyStatusId);
            _residentRepository.Insert(resident);

            Guid newUserId = Guid.NewGuid();
            var firstName = new Domain.Entities.Users.FirstName(request.FirstName);
            var lastName = new LastName(request.LastName);
            var email = new Email(request.Email);
            var contactNumber = new ContactNumber(request.ContactNumber);


            var userResident = new User(newUserId, request.ApartmentId, firstName, lastName, email, contactNumber, request.ResidentTypeId == (int)ResidentType.OWNER);

            _userRepository.Insert(userResident);

            var user = new UserDto(newUserId, request.ApartmentId, firstName, lastName, email, contactNumber);

            await _salesServiceClient.CreateUserAsync(user);

            int nextresidentUnitId = _residentUnitRepository.GetTotalRecordsCount() + 1;

            residentUnit = new ResidentUnit(nextresidentUnitId, request.UnitId, resident.Id); 
            _residentUnitRepository.Insert(residentUnit);
        }

        await _mediator.Publish(new ResidentCreatedEvent(resident.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(resident.Id));
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

    private Address GetAddress(ResidentAddress address, Guid cityId)
    {
        return new Address(Guid.NewGuid(), address.CountryId, address.StateId, cityId, address.AddressLine1, address.AddressLine2, address.ZipCode);
    }

    #endregion

}
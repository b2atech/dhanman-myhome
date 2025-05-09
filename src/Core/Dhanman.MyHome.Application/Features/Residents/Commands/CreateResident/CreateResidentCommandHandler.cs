using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants.Enums;
using Dhanman.MyHome.Application.Contracts;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Residents.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;
using Dhanman.MyHome.Domain.Entities.Cities;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Dhanman.MyHome.Domain.Entities.Users;
using MediatR;
using System.Threading.Tasks;
using ResidentAddress = Dhanman.MyHome.Application.Contracts.ServiceProviders.Address;

namespace Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;

public class CreateResidentCommandHandler : ICommandHandler<CreateResidentCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentRepository _residentRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IResidentUnitRepository _residentUnitRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISalesServiceClient _salesServiceClient;
    private readonly IPurchaseServiceClient _purchaseServiceClient;
    private readonly ICommonServiceClient _commonServiceClient;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CreateResidentCommandHandler(IResidentRepository residentRepository, IAddressRepository addressRepository, IResidentUnitRepository residentUnitRepository, IUserRepository userRepository, ICommonServiceClient commonServiceClient, ISalesServiceClient salesServiceClient, IPurchaseServiceClient purchaseServiceClient, IMediator mediator, IApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _residentRepository = residentRepository;
        _addressRepository = addressRepository;
        _residentUnitRepository = residentUnitRepository;
        _userRepository = userRepository;
        _commonServiceClient = commonServiceClient;
        _purchaseServiceClient = purchaseServiceClient;
        _salesServiceClient = salesServiceClient;
        _mediator = mediator;
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateResidentCommand request, CancellationToken cancellationToken)
    {
        ResidentUnit residentUnit;
        Resident resident = _residentRepository.GetByEmail(request.Email, request.ApartmentId);

        if (resident != null)
        {
           // var residentUnitId = _residentUnitRepository.GetTotalRecordsCount() + 1;
            residentUnit = new ResidentUnit(request.UnitId, resident.Id);
            _residentUnitRepository.Insert(residentUnit);
        }
        else
        {
            Guid? permanentAddressId = null;
            if (request.PermanentAddress != null)
            {
                Guid cityId = await GetCityId(request.PermanentAddress.CityName, request.PermanentAddress.ZipCode, request.PermanentAddress.StateId);

                 var permanentAddress = GetAddress(request.PermanentAddress, cityId);
                _addressRepository.Insert(permanentAddress);
                permanentAddressId = permanentAddress.Id;
            }


            Guid newUserId = Guid.NewGuid();

            resident = new Resident(request.ApartmentId, request.FirstName, request.LastName, request.Email, request.ContactNumber, permanentAddressId, newUserId, request.ResidentTypeId, request.OccupancyStatusId);
            _residentRepository.Insert(resident);
            await _unitOfWork.SaveChangesAsync();


            var firstName = new Domain.Entities.Users.FirstName(request.FirstName);
            var lastName = new LastName(request.LastName);
            var email = new Email(request.Email);
            var contactNumber = new ContactNumber(request.ContactNumber);


            var userResident = new User(newUserId, request.ApartmentId, firstName, lastName, email, contactNumber, request.ResidentTypeId == (int)ResidentType.OWNER);

            _userRepository.Insert(userResident);

            var user = new UserDto(newUserId, request.ApartmentId, firstName, lastName, email, contactNumber);

            await _commonServiceClient.CreateUserAsync(user);
            await _salesServiceClient.CreateUserAsync(user);
            await _purchaseServiceClient.CreateUserAsync(user);

            //TODO
            // this ensure no duplicate entry in DB but multiple calls
            //bool alreadyExists = _residentUnitRepository.Exists(resident.Id, request.UnitId);

            //if (!alreadyExists)
            //{
            //    residentUnit = new ResidentUnit(request.UnitId, resident.Id);
            //    _residentUnitRepository.Insert(residentUnit);
            //}

            residentUnit = new ResidentUnit(request.UnitId, resident.Id); 
            _residentUnitRepository.Insert(residentUnit);
        }

        await _mediator.Publish(new ResidentCreatedEvent(resident.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(resident.Id));
    }

    private async Task<Guid> GetCityId(string cityName, string zipCode, Guid stateId)
    {
        string lowerCityName = cityName.ToLower();
        var city = _dbContext.Set<City>().FirstOrDefault(x => x.Name.ToLower() == lowerCityName && x.ZipCode == zipCode);

        if (city == null)
        {
            city = new City(Guid.NewGuid(), stateId, zipCode, cityName);
            _dbContext.Set<City>().Add(city);
            await _unitOfWork.SaveChangesAsync();
        }
        return city.Id;
    }

    private Address GetAddress(ResidentAddress address, Guid cityId)
    {
        return new Address(Guid.NewGuid(), address.CountryId, address.StateId, cityId, address.AddressLine1, address.AddressLine2, address.ZipCode);
    }

    #endregion

}
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
using MediatR;
using ResidentAddress = Dhanman.MyHome.Application.Contracts.ServiceProviders.Address;

namespace Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;

public class CreateResidentCommandHandler : ICommandHandler<CreateResidentCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentRepository _residentRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IResidentUnitRepository _residentUnitRepository;
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CreateResidentCommandHandler(IResidentRepository residentRepository, IAddressRepository addressRepository, IResidentUnitRepository residentUnitRepository, IMediator mediator, IApplicationDbContext dbContext)
    {
        _residentRepository = residentRepository;
        _addressRepository = addressRepository;
        _residentUnitRepository = residentUnitRepository;
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

            resident = new Resident(nextresidentId, request.FirstName, request.LastName, request.Email, request.ContactNumber, permanentAddressId, request.ResidentTypeId, request.OccupancyStatusId);
            _residentRepository.Insert(resident);

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
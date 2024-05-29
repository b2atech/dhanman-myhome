using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Residents.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;
using Dhanman.MyHome.Domain.Entities.Cities;
using Dhanman.MyHome.Domain.Entities.Residents;
using MediatR;
using ResidentAddress = Dhanman.MyHome.Application.Contracts.ServiceProviders.Address;

namespace Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;

public class CreateResidentCommandHandler : ICommandHandler<CreateResidentCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentRepository _residentRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CreateResidentCommandHandler(IResidentRepository residentRepository, IAddressRepository addressRepository, IMediator mediator, IApplicationDbContext dbContext)
    {
        _residentRepository = residentRepository;
        _addressRepository = addressRepository;
        _mediator = mediator;
        _dbContext = dbContext;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateResidentCommand request, CancellationToken cancellationToken)
    {

        Guid permCityId;
        Guid cityId = GetCityId(request.PermanentAddress.CityName, request.PermanentAddress.ZipCode, request.PermanentAddress.StateId);
        permCityId = cityId;

        var permanentAddress = GetAddress(request.PermanentAddress, permCityId);
        _addressRepository.Insert(permanentAddress);

        int nextresidentId = _residentRepository.GetTotalRecordsCount() + 1;

        var resident = new Resident(nextresidentId, request.UnitId, request.FirstName, request.LastName, request.Email, request.ContactNumber, permanentAddress.Id, request.ResidentTypeId, request.OccupancyStatusId);

        _residentRepository.Insert(resident);

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
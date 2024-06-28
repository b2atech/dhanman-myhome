using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ServiceProviders.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;
using Dhanman.MyHome.Domain.Entities.Cities;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using MediatR;
using ServiceProviderAddress = Dhanman.MyHome.Application.Contracts.ServiceProviders.Address;

namespace Dhanman.MyHome.Application.Features.ServiceProviders.Commands.CreateServiceProvider;

public class CreateServiceProviderCommandHandler : ICommandHandler<CreateServiceProviderCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IServiceProviderRepository _serviceProviderRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMediator _mediator;
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CreateServiceProviderCommandHandler(IServiceProviderRepository serviceProviderRepository, IAddressRepository addressRepository, IMediator mediator, IApplicationDbContext dbContext)
    {
        _serviceProviderRepository = serviceProviderRepository;
        _addressRepository = addressRepository;
        _mediator = mediator;
        _dbContext = dbContext;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateServiceProviderCommand request, CancellationToken cancellationToken)
    {
        Guid permCityId, presCityId;

        if (request.PermanentAddress.CityName == request.PresentAddress.CityName && request.PermanentAddress.ZipCode == request.PresentAddress.ZipCode)
        {
            Guid cityId = GetCityId(request.PermanentAddress.CityName, request.PermanentAddress.ZipCode, request.PermanentAddress.StateId);
            permCityId = cityId;
            presCityId = cityId;
        }
        else
        {
            permCityId = GetCityId(request.PermanentAddress.CityName, request.PermanentAddress.ZipCode, request.PermanentAddress.StateId);
            presCityId = GetCityId(request.PresentAddress.CityName, request.PresentAddress.ZipCode, request.PresentAddress.StateId);
        }

        var permanentAddress = CreateAddressEntity(request.PermanentAddress, permCityId);
        _addressRepository.Insert(permanentAddress);

        var presentAddress = CreateAddressEntity(request.PresentAddress, presCityId);
        _addressRepository.Insert(presentAddress);

        int nextServiceProviderId = _serviceProviderRepository.GetTotalRecordsCount() + 1;

        var serviceProvider = new ServiceProvider(nextServiceProviderId, request.FirstName, request.LastName, request.Email, request.VisitingFrom, request.ContactNumber, permanentAddress.Id, presentAddress.Id, request.ServiceProviderTypeId, request.ServiceProviderSubTypeId, request.VehicleNumber, request.IdentityTypeId, request.IdentityNumber, request.ValidityDate, request.PoliceVerificationStatus, request.IsHireable, request.IsVisible, request.IsFrequentVisitor, request.ApartmentId, request.Pin);

        _serviceProviderRepository.Insert(serviceProvider);

        await _mediator.Publish(new ServiceProviderCreatedEvent(serviceProvider.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(serviceProvider.Id));
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
    private Address CreateAddressEntity(ServiceProviderAddress address, Guid cityId)
    {
        return new Address(Guid.NewGuid(), address.CountryId, address.StateId, cityId, address.AddressLine1, address.AddressLine2, address.ZipCode);
    }
    #endregion

}
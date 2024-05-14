using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ServiceProviders.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;
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
    #endregion

    #region Constructors
    public CreateServiceProviderCommandHandler(IServiceProviderRepository serviceProviderRepository, IAddressRepository addressRepository, IMediator mediator)
    {
        _serviceProviderRepository = serviceProviderRepository;
        _addressRepository = addressRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateServiceProviderCommand request, CancellationToken cancellationToken)
    {
        int nextServiceProviderId = _serviceProviderRepository.GetTotalRecordsCount() + 1;

        var permanentAddress = GetAddress(request.PermanentAddress);
        _addressRepository.Insert(permanentAddress);

        var presentAddress = GetAddress(request.PresentAddress);
        _addressRepository.Insert(presentAddress);

        var serviceProvider = new ServiceProvider(nextServiceProviderId, request.FirstName, request.LastName, request.Email, request.VisitingFrom, request.ContactNumber, permanentAddress.Id, presentAddress.Id, request.ServiceProviderTypeId, request.ServiceProviderSubTypeId, request.VehicleNumber, request.IdentityTypeId, request.IdentityNumber, request.ValidityDate, request.PoliceverificationStatus, request.IsHireable, request.IsVisible, request.IsFrequentVisitor, request.CreatedBy);

        _serviceProviderRepository.Insert(serviceProvider);

        await _mediator.Publish(new ServiceProviderCreatedEvent(serviceProvider.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(serviceProvider.Id));
    }
    
    private Address GetAddress(ServiceProviderAddress address)
    {
        return new Address(Guid.NewGuid(), address.CountryId, address.StateId, address.CityId, address.AddressLine1, address.AddressLine2, address.ZipCode);
    }

    #endregion

}
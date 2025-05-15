using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;
using Dhanman.MyHome.Domain.Entities.Cities;
using Dhanman.MyHome.Domain.Entities.ResidentRequests;
using MediatR;
using System.Threading.Tasks;
using ResidentRequestAddress = Dhanman.MyHome.Application.Contracts.ServiceProviders.Address;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Commands.CreateResidentRequest;

public class CreateResidentRequestCommandHandler : ICommandHandler<CreateResidentRequestCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentRequestRepository _residentRequestRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public CreateResidentRequestCommandHandler(IResidentRequestRepository residentRequestRepository, IAddressRepository addressRepository, IMediator mediator, IApplicationDbContext dbContext, IUnitOfWork unitOfWork)
    {
        _residentRequestRepository = residentRequestRepository;
        _addressRepository = addressRepository;
        _mediator = mediator;
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateResidentRequestCommand request, CancellationToken cancellationToken)
    {
        Guid addressId = Guid.Empty; 

        if (request.PermanentAddress != null)
        {
            Guid cityId = await GetCityId(request.PermanentAddress.CityName, request.PermanentAddress.ZipCode, request.PermanentAddress.StateId);
            var permanentAddress = GetAddress(request.PermanentAddress, cityId);
            _addressRepository.Insert(permanentAddress);
            addressId = permanentAddress.Id; 
        }

       // int nextResidentRequestId = _residentRequestRepository.GetTotalRecordsCount() + 1;

        int requestStatusId = ResidentRequestStatus.PENDING_REQUEST;

        var residentRequest = new ResidentRequest(
          //  nextResidentRequestId,
            request.UnitId,
            request.FirstName,
            request.LastName,
            request.Email,
            request.ContactNumber,
            addressId,
            requestStatusId,
            request.ResidentTypeId,
            request.OccupancyStatusId
        );

        _residentRequestRepository.Insert(residentRequest);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new ResidentRequestCreatedEvent(residentRequest.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(residentRequest.Id));
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

    private Address GetAddress(ResidentRequestAddress address, Guid cityId)
    {
        return new Address(Guid.NewGuid(), address.CountryId, address.StateId, cityId, address.AddressLine1, address.AddressLine2, address.ZipCode);
    }
    #endregion

}
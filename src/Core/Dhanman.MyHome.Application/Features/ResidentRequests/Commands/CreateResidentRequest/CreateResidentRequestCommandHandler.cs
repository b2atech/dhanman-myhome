using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Constants;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Addresses;
using Dhanman.MyHome.Domain.Entities.ResidentRequests;
using MediatR;
using ResidentRequestAddress = Dhanman.MyHome.Application.Contracts.ServiceProviders.Address;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Commands.CreateResidentRequest;

public class CreateResidentRequestCommandHandler : ICommandHandler<CreateResidentRequestCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentRequestRepository _residentRequestRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateResidentRequestCommandHandler(IResidentRequestRepository residentRequestRepository, IAddressRepository addressRepository, IMediator mediator)
    {
        _residentRequestRepository = residentRequestRepository;
        _addressRepository = addressRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateResidentRequestCommand request, CancellationToken cancellationToken)
    {
        int nextresidentRequestId = _residentRequestRepository.GetTotalRecordsCount() + 1;
        var permanentAddress = GetAddress(request.PermanentAddress);
        _addressRepository.Insert(permanentAddress);
        int requestStatusId = ResidentRequestStatus.PENDING_REQUEST;

        var residentRequest = new ResidentRequest(nextresidentRequestId, request.ApartmentId, request.BuildingId, request.FloorId, request.UnitId, request.FirstName, request.LastName, request.Email, request.ContactNumber, permanentAddress.Id, requestStatusId, request.ResidentTypeId, request.OccupancyStatusId, request.CreatedBy);

        _residentRequestRepository.Insert(residentRequest);

        await _mediator.Publish(new ResidentRequestCreatedEvent(residentRequest.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(residentRequest.Id));
    }

    private Address GetAddress(ResidentRequestAddress address)
    {
        return new Address(Guid.NewGuid(), address.CountryId, address.StateId, address.CityId, address.AddressLine1, address.AddressLine2, address.ZipCode);
    }
    #endregion

}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Domain.Entities.ResidentRequests;
using MediatR;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Commands.CreateResidentRequest;

public class CreateResidentRequestCommandHandler : ICommandHandler<CreateResidentRequestCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentRequestRepository _residentRequestRepository; 
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateResidentRequestCommandHandler(IResidentRequestRepository residentRequestRepository, IMediator mediator)
    {
        _residentRequestRepository = residentRequestRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateResidentRequestCommand request, CancellationToken cancellationToken)
    {
        int nextresidentRequestId = _residentRequestRepository.GetTotalRecordsCount() + 1;

        var residentRequest = new ResidentRequest(nextresidentRequestId, request.ApartmentId, request.BuildingId, request.FloorId, request.UnitId, request.FirstName, request.LastName, request.Email, request.ContactNumber, request.PermanentAddressId, request.RequestStatusId, request.ResidentTypeId, request.OccupancyStatusId, request.CreatedBy);

        _residentRequestRepository.Insert(residentRequest);

        await _mediator.Publish(new ResidentRequestCreatedEvent(residentRequest.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(residentRequest.Id));
    }
    #endregion

}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Residents.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Apartments;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Residents.Commands.CreateResident;

public class CreateResidentCommandHandler : ICommandHandler<CreateResidentCommand, Result<EntityCreatedResponse>>
{
    #region Properties
    private readonly IResidentRepository _residentRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateResidentCommandHandler(IResidentRepository residentRepository, IMediator mediator)
    {
        _residentRepository = residentRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methodes
    public async Task<Result<EntityCreatedResponse>> Handle(CreateResidentCommand request, CancellationToken cancellationToken)
    {
        int nextresidentId = _residentRepository.GetTotalRecordsCount() + 1;

        var resident = new Resident(nextresidentId, request.UnitId, request.FirstName, request.LastName, request.Email, request.ContactNumber, request.PermanentAddressId, request.ResidentTypeId, request.OccupancyStatusId, request.CreatedBy);

        _residentRepository.Insert(resident);

        await _mediator.Publish(new ResidentCreatedEvent(resident.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(resident.Id));
    }
    #endregion

}
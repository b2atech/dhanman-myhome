using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.ResidentRequests.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Events;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler : ICommandHandler<CreateEventCommand, Result<EntityCreatedResponse>>
{

    #region Properties
    private readonly IEventRepository  _eventRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateEventCommandHandler(IEventRepository eventRepository, IMediator mediator)
    {
        _eventRepository = eventRepository;
        _mediator = mediator;
    }
    #endregion


    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {


        var residentRequest = new Event(request.Id, request.Name, request.Description,request.IsFullDay,request.BackgroundColor,request.TextColor, request.UnitId, request.ReservationDate, request.StartDate, request.EndDate, request.Pourpose, request.StatusId, request.BookingFacilitiesId);

        _eventRepository.Insert(residentRequest);

        await _mediator.Publish(new ResidentRequestCreatedEvent(residentRequest.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(residentRequest.Id));



    }

    #endregion
}

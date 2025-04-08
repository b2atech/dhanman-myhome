using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Events.Events;
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
        var eventRequest = new Event(request.Id, request.Title, request.Description,request.AllDay,request.Color,request.TextColor, request.ReservationByUnitId, request.ReservationDate, request.Start, request.End, request.Pourpose, request.StatusId, request.BookingFacilitiesId);

        _eventRepository.Insert(eventRequest);

        await _mediator.Publish(new EventCreatedEvent(eventRequest.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(eventRequest.Id));
    }

    #endregion
}

using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.Events.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.Events;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Events.Commands.DeleteCommand;

public class DeleteEventCommandHandler : ICommandHandler<DeleteEventCommand, Result<EntityDeletedResponse>>
{
    #region Properties
    private readonly IEventRepository _eventRepository;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public DeleteEventCommandHandler(IEventRepository eventRepository, IMediator mediator)
    {
        _eventRepository = eventRepository;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityDeletedResponse>> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        Event existingEvent = await _eventRepository.GetBydIdAsync(request.id);
        _eventRepository.Delete(existingEvent);
        await _mediator.Publish(new EventCreatedEvent(existingEvent.Id), cancellationToken);
        return Result.Success(new EntityDeletedResponse(existingEvent.Id));
    }
    #endregion
}

using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
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
    private readonly IApplicationDbContext  _dbContext;
    private readonly IMediator _mediator;
    #endregion

    #region Constructors
    public CreateEventCommandHandler(IEventRepository eventRepository, IApplicationDbContext dbContext, IMediator mediator)
    {
        _eventRepository = eventRepository;
        _dbContext = dbContext;
        _mediator = mediator;
    }
    #endregion

    #region Methods
    public async Task<Result<EntityCreatedResponse>> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var eventEntity = new Event(
                request.Id,
                request.CompanyId,
                request.CommunityCalenderId,
                request.Title,
                request.Description,
                request.StartTime,
                request.EndTime,
                request.IsRecurring,
                request.RecurrenceRule,
                request.RecurrenceRuleId,
                request.RecurrenceEndDate
        );

        _eventRepository.Insert(eventEntity);

        await _mediator.Publish(new EventCreatedEvent(eventEntity.Id), cancellationToken);
        return Result.Success(new EntityCreatedResponse(eventEntity.Id));
    }

    #endregion
}


using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Entities.MeetingAgendaItems;
using Dhanman.MyHome.Application.Features.MeetingAgendaItems.Events;
using MediatR;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Commands.CreateMeetingAgendaItem;

public class CreateMeetingAgendaItemCommandHandler : ICommandHandler<CreateMeetingAgendaItemCommand, Result<EntityCreatedResponse>>
{
    private readonly IMeetingAgendaItemRepository _repository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMeetingAgendaItemCommandHandler(IMeetingAgendaItemRepository repository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EntityCreatedResponse>> Handle(CreateMeetingAgendaItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new MeetingAgendaItem(request.OccurrenceId, request.ItemText, request.OrderNo);

        _repository.Insert(entity);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new MeetingAgendaItemCreatedEvent(entity.Id), cancellationToken);

        return Result.Success(new EntityCreatedResponse(entity.Id));
    }
}
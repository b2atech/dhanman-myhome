using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.EventOccurrences.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Commands.DeleteMeetingAgendaItem;

public sealed class DeleteMeetingAgendaItemCommandHandler : ICommandHandler<DeleteMeetingAgendaItemCommand, Result<EntityDeletedResponse>>
{
    private readonly IMeetingAgendaItemRepository _meetingAgendaItemRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteMeetingAgendaItemCommandHandler(IMeetingAgendaItemRepository meetingAgendaItemRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _meetingAgendaItemRepository = meetingAgendaItemRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<EntityDeletedResponse>> Handle(DeleteMeetingAgendaItemCommand request, CancellationToken cancellationToken)
    {
        var meetingAgendaItem = await _meetingAgendaItemRepository.GetByIntIdAsync(request.MeetingAgendaItemId);
        if (meetingAgendaItem == null)
        {
            throw new EventOccurrenceNotFoundException(request.MeetingAgendaItemId);
        }
        _meetingAgendaItemRepository.Delete(meetingAgendaItem);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        await _mediator.Publish(new EventOccurrenceDeletedEvent(meetingAgendaItem.Id), cancellationToken);
        return Result.Success(new EntityDeletedResponse(meetingAgendaItem.Id));
    }
}


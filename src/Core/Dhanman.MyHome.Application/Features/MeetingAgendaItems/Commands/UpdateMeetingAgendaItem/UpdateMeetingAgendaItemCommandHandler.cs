using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Features.MeetingAgendaItems.Events;
using Dhanman.MyHome.Domain.Abstractions;
using Dhanman.MyHome.Domain.Exceptions;
using MediatR;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Commands.UpdateMeetingAgendaItem;

public sealed class UpdateMeetingAgendaItemCommandHandler : ICommandHandler<UpdateMeetingAgendaItemCommand, Result<EntityUpdatedResponse>>
{
    private readonly IMeetingAgendaItemRepository _meetingAgendaItemRepository;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateMeetingAgendaItemCommandHandler(IMeetingAgendaItemRepository meetingAgendaItemRepository, IMediator mediator, IUnitOfWork unitOfWork)
    {
        _meetingAgendaItemRepository = meetingAgendaItemRepository;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EntityUpdatedResponse>> Handle(UpdateMeetingAgendaItemCommand request, CancellationToken cancellationToken)
    {
        var meetingAgendaItem = await _meetingAgendaItemRepository.GetByIntIdAsync(request.Id);

        if (meetingAgendaItem == null)
        {
            throw new MeetingAgendaItemNotFoundException(request.Id);
        }
        meetingAgendaItem.OccurrenceId = request.OccurrenceId;
        meetingAgendaItem.ItemText = request.ItemText;
        meetingAgendaItem.OrderNo = request.OrderNo;


        _meetingAgendaItemRepository.Update(meetingAgendaItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new MeetingAgendaItemUpdatedEvent(meetingAgendaItem.Id), cancellationToken);

        return Result.Success(new EntityUpdatedResponse(meetingAgendaItem.Id));
    }
}

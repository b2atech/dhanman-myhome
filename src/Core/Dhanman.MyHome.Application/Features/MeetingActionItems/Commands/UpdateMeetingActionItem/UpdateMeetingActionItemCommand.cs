using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.MeetingActionItems;

namespace Dhanman.MyHome.Application.Features.MeetingActionItems.Commands.UpdateMeetingActionItem;

public sealed class UpdateMeetingActionItemCommand : ICommand<Result<EntityUpdatedResponse>>
{
    public Guid EventId { get; set; }
    public DateOnly OccurrenceDate { get; set; }
    public ActionItem[] ActionItems { get; set; }


    public UpdateMeetingActionItemCommand() { }

    public UpdateMeetingActionItemCommand(Guid eventId, DateOnly occurrenceDate, ActionItem[] actionItems)
    {
        EventId = eventId;
        OccurrenceDate = occurrenceDate;
        ActionItems = actionItems;
    }
}

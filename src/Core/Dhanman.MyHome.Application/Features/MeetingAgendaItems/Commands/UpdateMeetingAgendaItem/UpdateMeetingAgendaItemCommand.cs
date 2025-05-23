using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;
using Dhanman.MyHome.Application.Contracts.MeetingAgendaItems;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Commands.UpdateMeetingAgendaItem;

public sealed class UpdateMeetingAgendaItemCommand : ICommand<Result<EntityUpdatedResponse>>
{
    public Guid EventId { get; set; }
    public DateOnly OccurrenceDate { get; set; }
    public AgendaItem[] AgendaItems { get; set; }


    public UpdateMeetingAgendaItemCommand() { }

    public UpdateMeetingAgendaItemCommand(Guid eventId, DateOnly occurrenceDate, AgendaItem[] agendaItems)
    {
        EventId = eventId;
        OccurrenceDate = occurrenceDate;
        AgendaItems = agendaItems;
    }
}
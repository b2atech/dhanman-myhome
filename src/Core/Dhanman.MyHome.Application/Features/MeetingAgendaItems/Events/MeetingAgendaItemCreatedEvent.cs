using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Events;

public class MeetingAgendaItemCreatedEvent : IEvent
{
    public int MeetingAgendaItemId { get; }

    public MeetingAgendaItemCreatedEvent(int meetingAgendaItemId) => MeetingAgendaItemId = meetingAgendaItemId;
}

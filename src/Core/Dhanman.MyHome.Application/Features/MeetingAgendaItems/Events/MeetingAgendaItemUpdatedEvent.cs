using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Events;

internal class MeetingAgendaItemUpdatedEvent : IEvent
{
    public int MeetingAgendaItemId { get; }

    public MeetingAgendaItemUpdatedEvent(int meetingAgendaItemId) => MeetingAgendaItemId = meetingAgendaItemId;
}

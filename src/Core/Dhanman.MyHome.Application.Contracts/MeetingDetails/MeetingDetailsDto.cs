using Dhanman.MyHome.Application.Contracts.MeetingParticipants;

namespace Dhanman.MyHome.Application.Contracts.MeetingDetails;

public class MeetingDetailsDto
{
    public List<MeetingParticipantDto> ParticipantItems { get; set; } = new();
    public List<MeetingAgendaDto> AgendaItems { get; set; } = new();
    public List<MeetingActionItemDto> ActionItems { get; set; } = new();
    public string? NoteText { get; set; }
}

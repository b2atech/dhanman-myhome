namespace Dhanman.MyHome.Application.Contracts.MeetingDetails;

public class MeetingDetailsDto
{
   // public Guid EventId { get; set; }
  //  public DateOnly OccurrenceDate { get; set; }

    public List<Guid> ParticipantUserIds { get; set; } = new();
    public List<MeetingAgendaDto> AgendaItems { get; set; } = new();
    public List<MeetingActionItemDto> ActionItems { get; set; } = new();
    public string? NoteText { get; set; }
}

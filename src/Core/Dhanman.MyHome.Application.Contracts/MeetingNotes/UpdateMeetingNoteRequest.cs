namespace Dhanman.MyHome.Application.Contracts.MeetingNotes;

public sealed class UpdateMeetingNoteRequest
{
    #region Properties
    public Guid EventId { get; set; }
    public DateOnly OccurrenceDate { get; set; }
    public string NoteText { get; set; }
    #endregion
}

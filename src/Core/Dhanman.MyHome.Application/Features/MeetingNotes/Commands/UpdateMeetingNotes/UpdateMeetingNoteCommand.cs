using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.MeetingNotes.Commands.UpdateMeetingNotes;

public sealed class UpdateMeetingNoteCommand : ICommand<Result<EntityUpdatedResponse>>
{
    public Guid EventId { get; set; }
    public DateOnly OccurrenceDate { get; set; }
    public string NoteText { get; set; }


    public UpdateMeetingNoteCommand() { }

    public UpdateMeetingNoteCommand(Guid eventId, DateOnly occurrenceDate, string noteText)
    {
        EventId = eventId;
        OccurrenceDate = occurrenceDate;
        NoteText = noteText;
    }
}

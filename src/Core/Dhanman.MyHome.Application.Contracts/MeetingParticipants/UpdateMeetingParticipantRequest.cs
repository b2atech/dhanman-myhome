namespace Dhanman.MyHome.Application.Contracts.MeetingParticipants;

public class UpdateMeetingParticipantRequest
{
    #region Properties
    public Guid EventId { get; set; }
    public DateOnly OccurrenceDate { get; set; }
    public List<Guid> UserIds { get; set; }
    #endregion

    #region Constructor
    public UpdateMeetingParticipantRequest()
    {

    }
    #endregion
}
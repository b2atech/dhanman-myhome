namespace Dhanman.MyHome.Application.Contracts.MeetingParticipants;

public class UpdateMeetingParticipantRequest
{
    #region Properties
    public int OccurrenceId { get; set; }
    public List<Guid> UserIds { get; set; }
    public string Role { get; set; }
    #endregion

    #region Constructor
    public UpdateMeetingParticipantRequest()
    {

    }
    #endregion
}
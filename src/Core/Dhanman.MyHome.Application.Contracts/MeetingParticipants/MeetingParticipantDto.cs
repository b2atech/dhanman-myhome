namespace Dhanman.MyHome.Application.Contracts.MeetingParticipants;

public class MeetingParticipantDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }

    public string UserName { get; set; }
}
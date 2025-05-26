namespace Dhanman.MyHome.Application.Contracts.MeetingDetails;

public class MeetingActionItemDto
{
    public int Id { get; set; }
    public string ActionDescription { get; set; }
    public Guid AssignedToUserId { get; set; }
    public string AssignedToUserName { get; set; }
}

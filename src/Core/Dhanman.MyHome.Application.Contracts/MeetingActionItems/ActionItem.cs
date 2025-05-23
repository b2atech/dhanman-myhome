namespace Dhanman.MyHome.Application.Contracts.MeetingActionItems;
    public class ActionItem
{
    public int Id { get; set; }
    public string ActionDescription { get; set; } = string.Empty;
    public Guid AssignedToUserId { get; set; }

}

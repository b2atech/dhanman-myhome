namespace Dhanman.MyHome.Application.Contracts.MeetingActionItems;
    public class ActionItem
{
    public string Id { get; set; } = "-1"; // "-1" for new items
    public string ActionDescription { get; set; } = string.Empty;
    public Guid AssignedToUserId { get; set; }

}

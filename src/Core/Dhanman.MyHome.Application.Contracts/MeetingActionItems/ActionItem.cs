namespace Dhanman.MyHome.Application.Contracts.MeetingActionItems;
    public class ActionItem
{
    public int Id { get; set; } = -1; 
    public string ActionDescription { get; set; } = string.Empty;
    public Guid AssignedToUserId { get; set; }

}

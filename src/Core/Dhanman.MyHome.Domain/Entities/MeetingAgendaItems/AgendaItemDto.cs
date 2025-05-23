namespace Dhanman.MyHome.Domain.Entities.MeetingAgendaItems;

public class AgendaItemDto
{
    public string? Id { get; set; } 
   
    public string ItemText { get; set; } = string.Empty;
    
    public int OrderNo { get; set; }
}

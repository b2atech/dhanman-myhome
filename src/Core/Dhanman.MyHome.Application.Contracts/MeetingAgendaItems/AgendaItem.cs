namespace Dhanman.MyHome.Application.Contracts.MeetingAgendaItems;

public class AgendaItem
{
    public int Id { get; set; } = -1;
    public string ItemText { get; set; }
    public int OrderNo { get; set; }

    public AgendaItem(int id, string itemText, int orderNo)
    {
        Id = id;
        ItemText = itemText;
        OrderNo = orderNo;
    }
}

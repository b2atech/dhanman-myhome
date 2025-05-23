namespace Dhanman.MyHome.Application.Contracts.MeetingAgendaItems;

public sealed class CreateMeetingAgendaItemRequest
{
    #region Properties
    public Guid EventId { get; set; }
    public string ItemText { get; set; } = string.Empty;
    public int OrderNo { get; set; }
    #endregion

    #region Constructors
    public CreateMeetingAgendaItemRequest()
    {

    }
    #endregion
}

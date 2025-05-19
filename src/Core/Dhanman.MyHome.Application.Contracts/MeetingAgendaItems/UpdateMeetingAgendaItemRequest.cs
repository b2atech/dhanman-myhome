namespace Dhanman.MyHome.Application.Contracts.MeetingAgendaItems;

public sealed class UpdateMeetingAgendaItemRequest
{
    #region Properties
    public int Id { get; set; }
    public int OccurrenceId { get; set; }
    public string ItemText { get; set; } = string.Empty;
    public int OrderNo { get; set; }
    #endregion

    #region Constructors
    public UpdateMeetingAgendaItemRequest()
    {

    }
    #endregion
}

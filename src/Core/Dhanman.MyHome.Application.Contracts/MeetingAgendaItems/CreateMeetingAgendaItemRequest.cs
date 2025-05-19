namespace Dhanman.MyHome.Application.Contracts.MeetingAgendaItems;

public sealed class CreateMeetingAgendaItemRequest
{
    #region Properties
    public int OccurrenceId { get; set; }
    public string ItemText { get; set; } = string.Empty;
    public int OrderNo { get; set; }
    public Guid CreatedBy { get; set; }
    #endregion

    #region Constructors
    public CreateMeetingAgendaItemRequest()
    {

    }
    #endregion
}

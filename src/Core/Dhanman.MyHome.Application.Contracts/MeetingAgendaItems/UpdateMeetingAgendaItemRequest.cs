namespace Dhanman.MyHome.Application.Contracts.MeetingAgendaItems;

public sealed class UpdateMeetingAgendaItemRequest
{
    #region Properties
    public Guid EventId { get; set; }
    public DateOnly OccurrenceDate { get; set; }
    public AgendaItem[] AgendaItems { get; set; }
    #endregion

    #region Constructors
    public UpdateMeetingAgendaItemRequest()
    {

    }
    #endregion
}

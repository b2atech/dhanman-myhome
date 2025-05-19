namespace Dhanman.MyHome.Application.Contracts.EventOccurrences;

public class CreateEventOccurrenceRequest
{
    #region Properties
    public Guid EventId { get; set; }
    public DateTime OccurrenceDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool GeneratedFromRecurrence { get; set; }
    public int EventOccurrenceStatusId { get; set; }
    public string? RecordingUrl { get; set; }
    public string? Notes { get; set; }
    public Guid CreatedBy { get; set; }
    #endregion

    #region Constructors
    public CreateEventOccurrenceRequest()
    {

    }
    #endregion
}

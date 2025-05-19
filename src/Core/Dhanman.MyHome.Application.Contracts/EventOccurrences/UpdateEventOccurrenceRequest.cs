namespace Dhanman.MyHome.Application.Contracts.EventOccurrences;

public sealed class UpdateEventOccurrenceRequest
{
    #region Properties
    public int Id { get; set; }
    public Guid EventId { get; set; }
    public DateTime OccurrenceDate { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool GeneratedFromRecurrence { get; set; }
    public int EventOccurrenceStatusId { get; set; }
    public string? RecordingUrl { get; set; }
    public string? Notes { get; set; }
    #endregion

    public UpdateEventOccurrenceRequest() { }
}

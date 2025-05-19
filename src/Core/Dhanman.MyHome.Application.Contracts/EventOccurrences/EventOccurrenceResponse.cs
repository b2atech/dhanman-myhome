namespace Dhanman.MyHome.Application.Contracts.EventOccurrences;

public sealed class EventOccurrenceResponse
{
    #region Properties 
    public int Id { get; }
    public Guid EventId { get; }
    public string EventName { get; }
    public DateTime OccurrenceDate { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public bool GeneratedFromRecurrence { get; }
    public int EventOccurrenceStatusId { get; set; }
    public string EventOccurrenceStatusName { get; set; }
    public string? RecordingUrl { get; set; }
    public string? Notes { get; set; }
    #endregion


    #region Audit Properties
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }
    public string CreatedByName { get; }
    public string? ModifiedByName { get; }
    #endregion

    #region Constructors
    public EventOccurrenceResponse()
    {

    }

    public EventOccurrenceResponse(int id, Guid eventId, string eventName, DateTime occurrenceDate, DateTime startTime, DateTime endTime, bool generatedFromRecurrence, int eventOccurrenceStatusId, string eventOccurrenceStatusName, string? recordingUrl, string? notes, DateTime createdOnUtc, DateTime? modifiedOnUtc, Guid createdBy, Guid? modifiedBy, string createdByName, string? modifiedByName)
    {
        Id = id;
        EventId = eventId;
        EventName = eventName;
        OccurrenceDate = occurrenceDate;
        StartTime = startTime;
        EndTime = endTime;
        GeneratedFromRecurrence = generatedFromRecurrence;
        EventOccurrenceStatusId = eventOccurrenceStatusId;
        EventOccurrenceStatusName = eventOccurrenceStatusName;
        RecordingUrl = recordingUrl;
        Notes = notes;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        CreatedByName = createdByName;
        ModifiedByName = modifiedByName;
    }


    #endregion
}

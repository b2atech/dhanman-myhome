using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.EventOccurrences;

public class EventOccurrence : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public Guid EventId { get; set; }              // FK to Events
    public DateTime OccurrenceDate { get; set; }  // Date only
    public DateTime StartTime { get; set; }        // Timestamp
    public DateTime EndTime { get; set; }          // Timestamp
    public bool GeneratedFromRecurrence { get; set; }
    public int EventOccurrenceStatusId { get; set; }              // Scheduled, Completed, Cancelled
    public string? RecordingUrl { get; set; }       // Optional
    public string? Notes { get; set; }              // Rich text notes
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; set; }
    #endregion

    #region Constructor
    public EventOccurrence(int id, Guid eventId, DateTime occurrenceDate, DateTime startTime, DateTime endTime, bool generatedFromRecurrence, int eventOccurrenceStatusId, string? recordingUrl, string? notes, Guid? modifiedBy)
    {
        Id = id;
        EventId = eventId;
        OccurrenceDate = occurrenceDate;
        StartTime = startTime;
        EndTime = endTime;
        GeneratedFromRecurrence = generatedFromRecurrence;
        EventOccurrenceStatusId = eventOccurrenceStatusId;
        RecordingUrl = recordingUrl;
        Notes = notes;
        ModifiedBy = modifiedBy;
    }
    #endregion
}

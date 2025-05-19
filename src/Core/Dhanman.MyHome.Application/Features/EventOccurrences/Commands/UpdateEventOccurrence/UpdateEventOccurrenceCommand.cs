using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.EventOccurrences.Commands.UpdateEventOccurrence;

public sealed class UpdateEventOccurrenceCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public int Id { get; }
    public Guid EventId { get; }
    public DateTime OccurrenceDate { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public bool GeneratedFromRecurrence { get; }
    public int EventOccurrenceStatusId { get; }
    public string? RecordingUrl { get; }
    public string? Notes { get; }
    #endregion

    #region Constructors
    public UpdateEventOccurrenceCommand(int id, Guid eventId, DateTime occurrenceDate, DateTime startTime, DateTime endTime, bool generatedFromRecurrence, int eventOccurrenceStatusId, string? recordingUrl, string? notes)
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
    }
    #endregion
}

using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.Shared.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.EventOccurrences.Commands.CreateEventOccurrence;

public sealed class CreateEventOccurrenceCommand : ICommand<Result<EntityCreatedResponse>>
{
    public int Id { get; }
    public Guid EventId { get; }
    public DateTime OccurrenceDate { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public bool GeneratedFromRecurrence { get; }
    public int EventOccurrenceStatusId { get; }
    public string? RecordingUrl { get; }
    public string? Notes { get; }
    public Guid CreatedBy { get; }

    public CreateEventOccurrenceCommand(Guid eventId, DateTime occurrenceDate, DateTime startTime, DateTime endTime, bool generatedFromRecurrence, int eventOccurrenceStatusId, string? recordingUrl, string? notes, Guid createdBy)
    {
        EventId = eventId;
        OccurrenceDate = occurrenceDate;
        StartTime = startTime;
        EndTime = endTime;
        GeneratedFromRecurrence = generatedFromRecurrence;
        EventOccurrenceStatusId = eventOccurrenceStatusId;
        RecordingUrl = recordingUrl;
        Notes = notes;
        CreatedBy = createdBy;
    }
}
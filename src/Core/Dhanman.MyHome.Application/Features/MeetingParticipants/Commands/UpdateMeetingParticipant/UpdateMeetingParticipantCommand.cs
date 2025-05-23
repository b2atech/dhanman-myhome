using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.MeetingParticipants.Commands.UpdateMeetingParticipant;

public class UpdateMeetingParticipantCommand : ICommand<Result<EntityUpdatedResponse>>
{
    #region Properties
    public Guid EventId { get; set; }
    public DateOnly OccurrenceDate { get; set; }
    public List<Guid> UserIds { get; set; } = new();
    #endregion

    #region Constructor
    public UpdateMeetingParticipantCommand(Guid eventId, DateOnly occurrenceDate, List<Guid> userIds)
    {
        EventId = eventId;
        OccurrenceDate = occurrenceDate;
        UserIds = userIds;
    }
    #endregion
}
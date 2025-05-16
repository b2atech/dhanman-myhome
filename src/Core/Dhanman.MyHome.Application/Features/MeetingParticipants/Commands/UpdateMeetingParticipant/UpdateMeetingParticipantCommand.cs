using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.MeetingParticipants.Commands.UpdateMeetingParticipant;

public class UpdateMeetingParticipantCommand : ICommand<Result<EntityCreatedResponse>>
{
    #region Properties
    public int OccurrenceId { get; set; }
    public List<Guid> UserIds { get; set; }
    public string Role { get; set; }
    #endregion

    #region Constructor
    public UpdateMeetingParticipantCommand(int occurrenceId, List<Guid> userIds, string role)
    {
        OccurrenceId = occurrenceId;
        UserIds = userIds;
        Role = role;
    }
    #endregion
}
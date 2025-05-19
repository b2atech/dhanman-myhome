using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Commands.DeleteMeetingAgendaItem;

public sealed class DeleteMeetingAgendaItemCommand : ICommand<Result<EntityDeletedResponse>>
{
    #region Properties
    public int MeetingAgendaItemId { get; }

    #endregion

    #region Constructors
    public DeleteMeetingAgendaItemCommand(int meetingAgendaItemId)
    {
        MeetingAgendaItemId = meetingAgendaItemId;
    }
    #endregion
}

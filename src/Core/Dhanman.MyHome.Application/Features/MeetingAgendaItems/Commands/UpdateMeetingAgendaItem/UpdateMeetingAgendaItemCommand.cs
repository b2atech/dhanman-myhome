using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Commands.UpdateMeetingAgendaItem;

public sealed class UpdateMeetingAgendaItemCommand : ICommand<Result<EntityUpdatedResponse>>
{
    public int Id { get; set; }
    public int OccurrenceId { get; set; }
    public string ItemText { get; set; }
    public int OrderNo { get; set; }

    public UpdateMeetingAgendaItemCommand(int id, int occurrenceId, string itemText, int orderNo)
    {
        Id = id;
        OccurrenceId = occurrenceId;
        ItemText = itemText;
        OrderNo = orderNo;
    }
}
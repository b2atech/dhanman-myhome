using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Common;

namespace Dhanman.MyHome.Application.Features.MeetingAgendaItems.Commands.CreateMeetingAgendaItem;

public  class CreateMeetingAgendaItemCommand : ICommand<Result<EntityCreatedResponse>>
{
    public int Id { get; set; }
    public int OccurrenceId { get; set; }
    public string ItemText { get; set; }
    public int OrderNo { get; set; }
    

    public CreateMeetingAgendaItemCommand( int occurrenceId, string itemText, int orderNo)
    {
        OccurrenceId = occurrenceId;
        ItemText = itemText;
        OrderNo = orderNo;
    }
}
using Dhanman.MyHome.Application.Contracts.MeetingActionItems;

namespace Dhanman.MyHome.Application.Contracts.MeetingParticipants;

public class UpdateMeetingActionItemRequest
{
    #region Properties
    public Guid EventId { get; set; }
    public DateOnly OccurrenceDate { get; set; }
    public ActionItem[] ActionItems { get; set; }
    #endregion

    #region Constructor
    public UpdateMeetingActionItemRequest()
    {

    }
    #endregion
}
using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.MeetingAgendaItems;
public class MeetingAgendaItem : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int OccurrenceId { get; set; }  // FK to event_occurrences
    public string ItemText { get; set; }    // Rich text agenda item
    public int OrderNo { get; set; }        // Order number for sorting
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public Guid CreatedBy { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    #endregion

    #region Constructor
    public MeetingAgendaItem(int occurrenceId, string itemText, int orderNo)
    {
        OccurrenceId = occurrenceId;
        ItemText = itemText;
        OrderNo = orderNo;
    }
    #endregion
}

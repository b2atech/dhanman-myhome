namespace Dhanman.MyHome.Application.Contracts.MeetingAgendaItems;

public class MeetingAgendaItemResponse
{
    #region Properties
    public int Id { get; set; }  
    public int OccurrenceId { get; set; }  
    public string ItemText { get; set; }   
    public int OrderNo { get; set; }    
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public Guid CreatedBy { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Guid? ModifiedBy { get; set; }
    public string CreatedByName { get; set; }
    public string? ModifiedByName { get; set; }
    #endregion

    #region Constructor
    public MeetingAgendaItemResponse(int id,int occurrenceId, string itemText, int orderNo, DateTime createdOnUtc, bool isDeleted, DateTime? deletedOnUtc, Guid createdBy, DateTime? modifiedOnUtc, Guid? modifiedBy, string createdByName, string? modifiedByName)
    {
        Id = id;
        OccurrenceId = occurrenceId;
        ItemText = itemText;
        OrderNo = orderNo;
        CreatedOnUtc = createdOnUtc;
        IsDeleted = isDeleted;
        DeletedOnUtc = deletedOnUtc;
        CreatedBy = createdBy;
        ModifiedOnUtc = modifiedOnUtc;
        ModifiedBy = modifiedBy;
        CreatedByName = createdByName;
        ModifiedByName = modifiedByName;
    }
    #endregion
}

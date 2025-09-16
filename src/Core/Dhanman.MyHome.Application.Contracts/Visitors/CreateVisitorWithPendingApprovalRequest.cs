namespace Dhanman.MyHome.Application.Contracts.Visitors;

public class CreateVisitorWithPendingApprovalRequest
{
    #region Properties
    public Guid ApartmentId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string ContactNumber { get; set; }
    public int VisitorTypeId { get; set; }
    public Guid CreatedBy { get; set; }
    public List<int> UnitIds { get; set; }
    #endregion
}

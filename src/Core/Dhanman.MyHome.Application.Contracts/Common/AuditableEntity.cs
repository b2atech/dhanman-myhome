namespace Dhanman.MyHome.Application.Contracts.Common;
public class AuditableEntity
{
    #region Properties
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    #endregion

    #region Constructor
    public AuditableEntity()
    {

    }
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.Notifications;

public class VisitorApprovalRequest
{
    #region Properties
    public int VisitorLogId { get; set; }
    #endregion

    #region Constructors
    public VisitorApprovalRequest(int visitorLogId)
    {
        VisitorLogId = visitorLogId;
    }
    #endregion

}

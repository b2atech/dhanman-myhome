namespace Dhanman.MyHome.Application.Contracts.Notifications;

public class RequestApprovalActionRequest
{
    #region Properties
    public int VisitorLogId { get; set; }
    #endregion

    #region Constructors
    public RequestApprovalActionRequest(int visitorLogId)
    {
        VisitorLogId = visitorLogId;
    }
    #endregion

}

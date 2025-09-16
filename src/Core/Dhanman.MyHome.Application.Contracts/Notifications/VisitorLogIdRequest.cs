namespace Dhanman.MyHome.Application.Contracts.Notifications;

public class VisitorLogIdRequest
{
    public int VisitorLogId { get; set; }

    public VisitorLogIdRequest( int visitorLogId)
    {
        VisitorLogId = visitorLogId;
    }
}

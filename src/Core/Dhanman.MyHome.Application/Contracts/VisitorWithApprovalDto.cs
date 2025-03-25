namespace Dhanman.MyHome.Application.Contracts;

public class VisitorWithApprovalDto
{
    public int VisitorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string ContactNumber { get; set; }

    // Approved Visitor Details
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan? EntryTime { get; set; }
    public TimeSpan? ExitTime { get; set; }

    // QR Code URL
    //public string QRCodeUrl { get; set; }
}

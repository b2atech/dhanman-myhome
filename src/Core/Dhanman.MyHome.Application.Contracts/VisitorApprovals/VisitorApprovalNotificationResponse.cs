namespace Dhanman.MyHome.Application.Contracts.VisitorApprovals;

public sealed class VisitorApprovalNotificationResponse
{
    #region Properties

    public int SentCount { get; }
    public int VisitorId { get; }
    public string FirstName { get; }
    public string LastName { get; }

    #endregion

    #region Constructors

    public VisitorApprovalNotificationResponse(
        int sentCount,
        int visitorId,
        string firstName,
        string lastName)
    {
        SentCount = sentCount;
        VisitorId = visitorId;
        FirstName = firstName;
        LastName = lastName;
    }

    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.Notifications;

public class RequestApprovalActionRequest
{
    #region Properties
    public int UnitId { get; set; }
    public string GuestName { get; set; }
    public int GuestId { get; set; }
    #endregion

    #region Constructors
    public RequestApprovalActionRequest(int unitId, string guestName, int guestId)
    {
        UnitId = unitId;
        GuestName = guestName;
        GuestId = guestId;
    }
    #endregion

}

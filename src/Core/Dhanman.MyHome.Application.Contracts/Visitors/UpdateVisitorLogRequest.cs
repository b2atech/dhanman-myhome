namespace Dhanman.MyHome.Application.Contracts.Visitors;

public sealed class UpdateVisitorLogRequest
{
    #region Proeprties
    public int Id { get; set; }
    public int CurrentStatusId { get; set; }
    public DateTime? ExitTime { get; set; }
    public int VisitorStatusId { get; set; }
    #endregion

    #region Contrunctor
    public UpdateVisitorLogRequest(int id, int currentStatusId, DateTime? exitTime, int visitorStatusId)
    {
        Id = id;
        CurrentStatusId = currentStatusId;
        ExitTime = exitTime;
        VisitorStatusId = visitorStatusId;
    }

    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.VisitorLogs;

public sealed class CreateVisitorLogRequest
{
    #region Properties
    public int VisitorId { get; set; }
    public List<VisitorLog> VisitorLogDetails { get; set; }   
    #endregion

    #region Constructor
    public CreateVisitorLogRequest() { }

    #endregion


}
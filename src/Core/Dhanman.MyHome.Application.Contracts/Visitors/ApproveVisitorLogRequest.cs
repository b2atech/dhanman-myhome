namespace Dhanman.MyHome.Application.Contracts.Visitors;

public sealed class ApproveVisitorLogRequest
{
    #region Proeprties
    public int Id { get; set; }
    #endregion

    #region Contrunctor
    public ApproveVisitorLogRequest(int id)
    {
        Id = id;
    }

    #endregion
}

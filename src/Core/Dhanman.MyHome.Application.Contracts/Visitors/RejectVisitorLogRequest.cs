namespace Dhanman.MyHome.Application.Contracts.Visitors;

public class RejectVisitorLogRequest
{
    #region Proeprties
    public int Id { get; set; }
    #endregion

    #region Contrunctor
    public RejectVisitorLogRequest(int id)
    {
        Id = id;
    }

    #endregion
}

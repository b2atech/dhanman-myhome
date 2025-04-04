namespace Dhanman.MyHome.Application.Contracts.Visitors;

public sealed class UpdateVisitorLogRequest
{
    #region Proeprties
    public List<int> Ids { get; set; }
    #endregion

    #region Contrunctor
    public UpdateVisitorLogRequest(List<int> ids)
    {
        Ids = ids;
    }

    #endregion
}

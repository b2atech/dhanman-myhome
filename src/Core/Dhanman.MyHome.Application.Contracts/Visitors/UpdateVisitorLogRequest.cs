namespace Dhanman.MyHome.Application.Contracts.Visitors;

public sealed class UpdateVisitorLogRequest
{
    #region Proeprties
    public int Id { get; set; }
    #endregion

    #region Contrunctor
    public UpdateVisitorLogRequest(int id)
    {
        Id = id;
    }

    #endregion
}

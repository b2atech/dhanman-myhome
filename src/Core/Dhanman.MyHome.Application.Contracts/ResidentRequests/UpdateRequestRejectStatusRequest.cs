namespace Dhanman.MyHome.Application.Contracts.ResidentRequests;

public class UpdateRequestRejectStatusRequest
{
    #region Properties
    public int Id { get; set; }
    #endregion

    #region Constructors
    public UpdateRequestRejectStatusRequest(int id)
    {
        Id = id;
    }
    #endregion
}
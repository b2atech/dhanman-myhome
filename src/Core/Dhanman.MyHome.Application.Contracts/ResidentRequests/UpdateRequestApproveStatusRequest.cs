namespace Dhanman.MyHome.Application.Contracts.ResidentRequests;

public class UpdateRequestApproveStatusRequest
{
    #region Properties
    public int Id { get; set; }      
    #endregion

    #region Constructors
    public UpdateRequestApproveStatusRequest(int id)
    {
        Id = id;      
    }
    #endregion
}
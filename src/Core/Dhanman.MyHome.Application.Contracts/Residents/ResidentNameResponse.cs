namespace Dhanman.MyHome.Application.Contracts.Residents;

public sealed class ResidentNameResponse
{
    #region Properties 
    public int Id { get; }    
    public string ResidentName { get; }
    public Guid UserId { get; }

    #endregion

    #region Constructor
    public ResidentNameResponse(int id, string residentName,Guid userId)
    {
        Id = id;        
        ResidentName = residentName;
        UserId = userId;
    }
    #endregion
}
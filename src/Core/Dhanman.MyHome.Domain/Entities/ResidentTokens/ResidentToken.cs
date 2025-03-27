using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.ResidentTokens;

public class ResidentToken : Entity
{
    #region Properties
    public int ResidentId { get; set; }
    public string FCMToken { get; set; }
    #endregion

    #region Constructors
    public ResidentToken() { }     
    
    public ResidentToken(Guid id,int residentId, string fCMToken)
    {
        ResidentId = residentId;
        FCMToken = fCMToken;
    }
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.CommunityCalenders;

public sealed class CommunityCalenderNameResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }
    #endregion

    #region Constructor
    public CommunityCalenderNameResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}

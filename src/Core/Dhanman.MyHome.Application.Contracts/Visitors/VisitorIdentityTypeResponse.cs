namespace Dhanman.MyHome.Application.Contracts.Visitors;
public sealed class VisitorIdentityTypeResponse
{
    #region Properties 

    public int Id { get; }
    public string Name { get; }
    #endregion

    #region Constructor
    public VisitorIdentityTypeResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}
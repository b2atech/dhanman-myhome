namespace Dhanman.MyHome.Application.Contracts.Visitors;

public sealed class VisitorTypeResponse
{
    #region Properties 

    public int Id { get; }
    public string Name { get; }
    #endregion

    #region Constructor
    public VisitorTypeResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}
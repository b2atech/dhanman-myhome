namespace Dhanman.MyHome.Application.Contracts.Floors;

public sealed class FloorNameResponse
{
    #region Properties

    public int Id { get; }
    public string Name { get; }
    #endregion

    #region Constructor
    public FloorNameResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}

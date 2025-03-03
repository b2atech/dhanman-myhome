namespace Dhanman.MyHome.Application.Contracts.Gates;

public sealed class GateTypeResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }

    #endregion

    #region Constructor
    public GateTypeResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}

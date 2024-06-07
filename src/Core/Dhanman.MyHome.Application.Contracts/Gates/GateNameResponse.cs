namespace Dhanman.MyHome.Application.Contracts.Gates;

public sealed class GateNameResponse
{
    #region Properties 

    public int Id { get; }
    public string Name { get; }

    #endregion

    #region Constructor
    public GateNameResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion


}

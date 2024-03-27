namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitNameResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }   // flat number 

    #endregion

    #region Constructor
    public UnitNameResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}
namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitByFloorIdResponse
{
    #region Properties 
    public int Id { get; set; }
    public string Name { get; set; }

    #endregion

    #region Constructor
    public UnitByFloorIdResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}

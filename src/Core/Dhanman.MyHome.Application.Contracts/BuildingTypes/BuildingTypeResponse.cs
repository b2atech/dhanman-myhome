namespace Dhanman.MyHome.Application.Contracts.BuildingTypes;

public sealed class BuildingTypeResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }

    #endregion

    #region Constructor
    public BuildingTypeResponse(int id, string name)
    {
        Id = id;
        Name = name;
    }
    #endregion
}

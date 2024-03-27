namespace Dhanman.MyHome.Application.Contracts.Buildings;

public sealed class BuildingNameResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }    
    #endregion

    #region Constructor
    public BuildingNameResponse(int id, string name)
    {
        Id = id;
        Name = name;        
    }
    #endregion
}
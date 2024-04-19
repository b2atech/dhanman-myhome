namespace Dhanman.MyHome.Application.Contracts.OccupancyTypes;

public sealed class OccupancyTypeResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }   

    #endregion

    #region Constructor
    public OccupancyTypeResponse(int id, string name)
    {
        Id = id;
        Name = name;        
    }
    #endregion
}
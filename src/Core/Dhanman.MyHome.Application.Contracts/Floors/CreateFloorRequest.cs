namespace Dhanman.MyHome.Application.Contracts.Floors;

public class CreateFloorRequest
{
    #region Properties
    public string Name { get; set; }
    public Guid ApartmentId { get; set; }
    public int BuildingId { get; set; }
    public int TotalUnits { get; set; }
    #endregion

    #region Constructor
    public CreateFloorRequest()
    {

    }
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.Buildings;

public class CreateBuildingRequest
{

    #region Properties
    public Guid ApartmentId { get; set; }
    public string Name { get; set; }
    public int BuildingTypeId { get; set; }
    public int TotalUnits { get; set; }
    #endregion


    #region Constructor
    public CreateBuildingRequest()
    {
    }
    #endregion
}

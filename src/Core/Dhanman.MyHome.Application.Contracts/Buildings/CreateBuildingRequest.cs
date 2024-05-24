namespace Dhanman.MyHome.Application.Contracts.Buildings;

public class CreateBuildingRequest
{

    #region Properties
    public Guid ApartmentId { get; set; }
    public string Name { get; set; }
    public int BuildingTypeId { get; set; }
    public int TotalUnits { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
    #endregion


    #region Constructor
    public CreateBuildingRequest()
    {
    }
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.Floors;

public sealed class UpdateFloorRequest
{
    #region Properties
    public int Id { get; set; }
    public string Name { get; set; }
    public int BuildingId { get; set; }
    public int TotalUnits { get; set; }
    #endregion

    #region Constructors
    public UpdateFloorRequest() => Name = string.Empty;
    #endregion
}

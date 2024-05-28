namespace Dhanman.MyHome.Application.Contracts.Floors;

public sealed class FloorResponse
{
    #region Properties

    public int Id { get; }
    public string Name { get; }
    public Guid ApartmentId { get; }
    public int BuildingId { get; }
    public int BuildingName { get; }
    public int TotalUnits { get; }
    public Guid CreatedBy { get; }
    public DateTime CreatedOnUtc { get; }
    public Guid? ModifiedBy { get; }
    public DateTime? ModifiedOnUtc { get; }

    #endregion

    #region Constructor
    public FloorResponse(int id, string name,Guid apartmentId, int buildingId, string buildingName, int totalUnits, Guid createdBy, DateTime createdOnUtc, Guid? modifiedBy, DateTime? modifiedOnUtc)
    {
        Id = id;
        Name = name;
        ApartmentId = apartmentId;
        BuildingId = buildingId;
        TotalUnits = totalUnits;
        CreatedBy = createdBy;
        CreatedOnUtc = createdOnUtc;
        ModifiedBy = modifiedBy;
        ModifiedOnUtc = modifiedOnUtc;
    }
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.Floors;

public sealed class FloorResponse
{
    #region Properties

    public int Id { get; }
    public string Name { get; }
    public Guid ApartmentId { get; }
    public int BuildingId { get; }
    public string BuildingName { get; }
    public int TotalUnits { get; }
    public Guid CreatedBy { get; }
    public DateTime CreatedOnUtc { get; }
    public Guid? ModifiedBy { get; }
    public DateTime? ModifiedOnUtc { get; }
    public string CreatedByName { get; }
    public string? ModifiedByName { get; }

    #endregion

    #region Constructor
    public FloorResponse(int id, string name,Guid apartmentId, int buildingId, string buildingName, int totalUnits, Guid createdBy, DateTime createdOnUtc, Guid? modifiedBy, DateTime? modifiedOnUtc, string createdByName, string? modifiedByName)
    {
        Id = id;
        Name = name;
        ApartmentId = apartmentId;
        BuildingId = buildingId;
        BuildingName = buildingName;
        TotalUnits = totalUnits;
        CreatedBy = createdBy;
        CreatedOnUtc = createdOnUtc;
        ModifiedBy = modifiedBy;
        ModifiedOnUtc = modifiedOnUtc;
        CreatedByName = createdByName;
        ModifiedByName = modifiedByName;
    }
    #endregion
}

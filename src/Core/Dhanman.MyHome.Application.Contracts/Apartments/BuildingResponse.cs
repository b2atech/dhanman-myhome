namespace Dhanman.MyHome.Application.Contracts.Apartments;

public sealed class BuildingResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }
    public int BuildingTypeId { get; }
    public string BuildingTypeName { get; }
    public Guid ApartmentId { get; }
    public string ApartmentName { get; }
    public int TotalUnits { get; }
    public Guid CreatedBy { get; }
    public DateTime CreatedOnUtc { get; }
    #endregion

    #region Constructor
    public BuildingResponse(int id, string name, int buildingTypeId, string buildingTypeName, Guid apartmentId, string apartmentName, int totalUnits, Guid createdBy)
    {
        Id = id;
        Name = name;
        BuildingTypeId = buildingTypeId;
        BuildingTypeName = buildingTypeName;
        ApartmentId = apartmentId;
        ApartmentName = apartmentName;
        TotalUnits = totalUnits;
        CreatedBy = createdBy;        
    }
    #endregion
}

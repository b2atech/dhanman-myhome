namespace Dhanman.MyHome.Application.Contracts.Buildings;

public sealed class BuildingResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }
    public int BuildingTypeId { get; }
    public string BuildingTypeName { get; }    
    public int TotalUnits { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }
    #endregion

    #region Constructor
    public BuildingResponse(int id, string name, int buildingTypeId, string buildingTypeName, int totalUnits, DateTime createdOnUtc, DateTime? modifiedOnUtc, Guid createdBy, Guid? modifiedBy)
    {
        Id = id;
        Name = name;
        BuildingTypeId = buildingTypeId;
        BuildingTypeName = buildingTypeName;      
        TotalUnits = totalUnits;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
    }     
    #endregion
}

namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitDetailResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }   // flat number 
    public int FloorId { get; }
    public string FloorNumber { get; }
    public int UnitTypeId { get; } // Flat Type
    public string UnitType { get; } // Flat Type
    public int OccupantTypeId { get; }
    public string OccupantType { get; }  
    public float Area { get; }
    public float BHKType { get; }
    public Guid AccountId { get; }
    public string PhoneExtention { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }
    #endregion

    #region Constructor
    public UnitDetailResponse(int id, string name, int floorId, string floorNumber, int unitTypeId, string unitType, int occupantTypeId, string occupantType, float area, float bHKType, Guid accountId, string phoneExtention, DateTime createdOnUtc, DateTime? modifiedOnUtc, Guid createdBy, Guid? modifiedBy)
    {
        Id = id;
        Name = name;
        FloorId = floorId;
        FloorNumber = floorNumber;
        UnitTypeId = unitTypeId;
        UnitType = unitType;
        OccupantTypeId = occupantTypeId;
        OccupantType = occupantType;       
        Area = area;
        BHKType = bHKType;
        AccountId = accountId;
        PhoneExtention = phoneExtention;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
    }
    #endregion
}
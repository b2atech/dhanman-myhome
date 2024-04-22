namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }   // flat number 
    public int FloorId { get; }
    public string FloorNumber { get; }
    public int UnitTypeId { get; } // Flat Type
    public string UnitType{ get; } // Flat Type
    public int OccupantTypeId { get; }
    public string OccupantType { get; }
    public int OccupancyTypeId { get; }
    public string OccupancyType { get; }
    public int NumberOfMembers { get; }
    public float Area { get;  }
    public float BHKType { get; }
    public string PhoneExtention { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }
    #endregion

    #region Constructor
    public UnitResponse(int id, string name, int floorId, string floorNumber, int unitTypeId, string unitType, int occupantTypeId, string occupantType, int occupancyTypeId, string occupancyType, int numberOfMembers, float area, float bHKType, string phoneExtention, DateTime createdOnUtc, DateTime? modifiedOnUtc, Guid createdBy, Guid? modifiedBy)
    {
        Id = id;
        Name = name;
        FloorId = floorId;
        FloorNumber = floorNumber;
        UnitTypeId = unitTypeId;
        UnitType = unitType;
        OccupantTypeId = occupantTypeId;
        OccupantType = occupantType;
        OccupancyTypeId = occupancyTypeId;
        OccupancyType = occupancyType;
        NumberOfMembers = numberOfMembers;
        Area = area;
        BHKType = bHKType;
        PhoneExtention = phoneExtention;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
    } 
    #endregion
}
namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitDetailResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }   // flat number 
    public Guid CustomerId { get; }
    public int FloorId { get; }
    public string FloorNumber { get; }
    public int UnitTypeId { get; } // Flat Type
    public string UnitType { get; } // Flat Type
    public int OccupantTypeId { get; }
    public string OccupantType { get; }  
    public decimal Area { get; }
    public decimal BHKType { get; }
    public Guid AccountId { get; }
    #endregion

    #region Constructor
    public UnitDetailResponse(int id, string name, Guid customerId, int floorId, string floorNumber, int unitTypeId, string unitType, int occupantTypeId, string occupantType, decimal area, decimal bHKType, Guid accountId)
    {
        Id = id;
        Name = name;
        CustomerId = customerId;
        FloorId = floorId;
        FloorNumber = floorNumber;
        UnitTypeId = unitTypeId;
        UnitType = unitType;
        OccupantTypeId = occupantTypeId;
        OccupantType = occupantType;       
        Area = area;
        BHKType = bHKType;
        AccountId = accountId;        
    }
    #endregion
}
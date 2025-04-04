﻿namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitResponse
{
    #region Properties 
    public int Id { get; }
    public string Name { get; }  
    public int FloorId { get; }
    public string FloorNumber { get; }
    public int BuildingId { get; }
    public int UnitTypeId { get; } 
    public string UnitType{ get; } 
    public Guid CustomerId{ get; } 
    public int OccupantTypeId { get; }
    public string OccupantType { get; }
    public int OccupancyTypeId { get; }
    public string OccupancyType { get; }
    public int NumberOfMembers { get; }
    public decimal Area { get;  }
    public decimal BHKType { get; }
    public int PhoneExtention { get; }
    public int EIntercom { get; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; }
    public Guid CreatedBy { get; }
    public Guid? ModifiedBy { get; }
    public string CreatedByName { get; }
    public string? ModifiedByName { get; }
    public string CustomerName { get; }
    public string ContactNumber { get; }
    #endregion

    #region Constructor
    public UnitResponse(int id, string name, int floorId, string floorNumber,int buildingId, int unitTypeId, string unitType,Guid customerId, int occupantTypeId, string occupantType, int occupancyTypeId, string occupancyType, int numberOfMembers, decimal area, decimal bHKType, int phoneExtention, DateTime createdOnUtc, DateTime? modifiedOnUtc, Guid createdBy, Guid? modifiedBy, string createdByName, string? modifiedByName)
    {
        Id = id;
        Name = name;
        FloorId = floorId;
        FloorNumber = floorNumber;
        BuildingId = buildingId;
        UnitTypeId = unitTypeId;
        UnitType = unitType;
        CustomerId = customerId;
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
        CreatedByName = createdByName;
        ModifiedByName = modifiedByName;
    }

    public UnitResponse(int id, string name, int floorId, int buildingId, int unitTypeId, Guid customerId, int occupantTypeId, int occupancyTypeId, decimal area, decimal bHKType, int phoneExtention, int eIntercom, DateTime createdOnUtc, DateTime? modifiedOnUtc, Guid createdBy, Guid? modifiedBy, string createdByName, string? modifiedByName, string customerName, string contactNumber)
 {
        Id = id;
        Name = name;
        FloorId = floorId;
        BuildingId = buildingId;
        UnitTypeId = unitTypeId;
        CustomerId = customerId;
        OccupantTypeId = occupantTypeId;
        OccupancyTypeId = occupancyTypeId;
        Area = area;
        BHKType = bHKType;
        PhoneExtention = phoneExtention;
        EIntercom = eIntercom;
        CreatedOnUtc = createdOnUtc;
        ModifiedOnUtc = modifiedOnUtc;
        CreatedBy = createdBy;
        ModifiedBy = modifiedBy;
        CreatedByName = createdByName;
        ModifiedByName = modifiedByName;
        CustomerName = customerName;
        ContactNumber = contactNumber;

    }


    #endregion
}
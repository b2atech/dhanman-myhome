using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Units;

public class Unit : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    public Guid ApartmentId { get; set; }
    public int BuildingId { get; set; }
    public int FloorId { get; set; }

    //Residential, Commercial, Facilities, Security, Business, Parcel, Non Member
    public int UnitTypeId { get; set; }
    public Guid AccountId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Area { get; set; }
    public decimal BHKType { get; set; }

    //Owner, Tenant, Family, Co-owner, Tenant Family, Don't know, Multitenant, Builder, Empty
    public int OccupantTypeId { get; set; }

    //Rented, Owned, Inactive,
    public int OccupancyTypeId { get; set; }
    public int PhoneExtention { get; set; }
    public int EIntercom { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? ModifiedBy { get; }
    #endregion

    #region Constructor
    public Unit(int id,string name, int buildingId, int floorId, int unitTypeId,
        int occupantTypeId, int occupancyTypeId,decimal area, decimal bhkType, int phoneExtention,
        int eIntercom, string latitude, string longitude)
    {
        Id = id;
        Name = name;
        BuildingId = buildingId;
        FloorId = floorId;
        UnitTypeId = unitTypeId;
        OccupantTypeId = occupantTypeId;
        OccupancyTypeId = occupancyTypeId;
        Area = area;
        BHKType = bhkType;
        PhoneExtention = phoneExtention;
        EIntercom = eIntercom;
        Latitude = latitude;
        Longitude = longitude;
    }

    public Unit(int id, string name, int floorId, int unitTypeId,
        int occupantTypeId, int occupancyTypeId, int phoneExtention,
        int eIntercom, string latitude, string longitude)
    {
        Id = id;
        Name = name;
        FloorId = floorId;
        UnitTypeId = unitTypeId;
        OccupantTypeId = occupantTypeId;
        OccupancyTypeId = occupancyTypeId;
        PhoneExtention = phoneExtention;
        EIntercom = eIntercom;
        Latitude = latitude;
        Longitude = longitude;
        CreatedOnUtc = DateTime.UtcNow;
    }
    #endregion
}

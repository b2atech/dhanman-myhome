using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Apartments;

public class Unit : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    public int FloorId { get; set; }
    
    //Residential, Commercial, Facilities, Security, Business, Parcel, Non Member
    public int UnitTypeId { get; set; }

    //Owner, Tenant, Family, Co-owner, Tenant Family, Don't know, Multitenant, Builder, Empty
    public int OccupantTypeId { get; set; }

    //Rented, Owned, Inactive,
    public int OccupancyTypeId { get; set; }
    public string PhoneExtention{ get; set; }
    
   

    public string eIntercom { get; set; }
    public string Latitude { get; set; }
    public string Longitude { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool IsDeleted { get; }

    public Guid CreatedBy { get; }

    public Guid? ModifiedBy { get;  }
    #endregion

    #region Constructor
    public Unit(int id, string name, int floorId, int unitTypeId,
        int occupantTypeId, int occupancyTypeId, string phoneExtention, 
        string eIntercom, string latitude, string longitude, Guid createdBy)
    {
        Id = id; 
        Name = name;
        FloorId = floorId;
        UnitTypeId = unitTypeId;
        OccupantTypeId = occupantTypeId;
        OccupancyTypeId = occupancyTypeId;
        PhoneExtention = phoneExtention;
        this.eIntercom = eIntercom;
        Latitude = latitude;
        Longitude = longitude;
        CreatedBy = createdBy;
        CreatedOnUtc = DateTime.UtcNow;
    }
    #endregion
}

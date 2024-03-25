using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Apartments;

public class Building : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    public int BuildingTypeId { get; set; }
    public Guid ApartmentId { get; set; }
    public int TotalUnits { get; set; }
    #endregion

    #region Audit Properties
    public DateTime CreatedOnUtc { get; }

    public DateTime? ModifiedOnUtc { get; }

    public DateTime? DeletedOnUtc { get; }

    public bool IsDeleted { get; }

    public Guid CreatedBy { get; }

    public Guid? ModifiedBy { get; }
    #endregion

    #region Constructor
    public Building(int id, string name, int buildingTypeId, Guid apartmentId, int totalUnits, Guid createdBy)
    {
        Id = id; 
        Name = name;
        BuildingTypeId = buildingTypeId;
        ApartmentId = apartmentId;
        TotalUnits = totalUnits;
        CreatedBy = createdBy;
        CreatedOnUtc = DateTime.UtcNow;

    }
    #endregion
}

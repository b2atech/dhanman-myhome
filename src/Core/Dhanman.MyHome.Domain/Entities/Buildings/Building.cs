using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Buildings;

public class Building : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    public int BuildingTypeId { get; set; }
    public Guid ApartmentId { get; set; }
    public int TotalUnits { get; set; }
    #endregion
    #region Audit Properties
    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }
    #endregion

    #region Constructor
    public Building(string name, int buildingTypeId, Guid apartmentId, int totalUnits)
    {
        //Id = id;
        ApartmentId = apartmentId;
        Name = name;
        BuildingTypeId = buildingTypeId;
        TotalUnits = totalUnits;
    }
    #endregion
}

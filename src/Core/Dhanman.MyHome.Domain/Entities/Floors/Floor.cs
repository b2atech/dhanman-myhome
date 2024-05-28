using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Floors;

public class Floor : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public string Name { get; set; }
    public Guid ApartmentId { get; set; }
    public int BuildingId { get; set; }
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
    public Floor(int id, string name, Guid apartmentId, int buildingId, int totalUnits)
    {
        Id = id;
        Name = name;
        ApartmentId = apartmentId;
        BuildingId = buildingId;
        TotalUnits = totalUnits;

    }
    #endregion
}

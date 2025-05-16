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
    public DateTime CreatedOnUtc { get; set; }

    public DateTime? ModifiedOnUtc { get; set; }

    public DateTime? DeletedOnUtc { get; set; }

    public bool IsDeleted { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? ModifiedBy { get; set; }
    #endregion

    #region Constructor
    public Floor(string name, Guid apartmentId, int buildingId, int totalUnits)
    {
       // Id = id;
        Name = name;
        ApartmentId = apartmentId;
        BuildingId = buildingId;
        TotalUnits = totalUnits;
    }
    #endregion
}

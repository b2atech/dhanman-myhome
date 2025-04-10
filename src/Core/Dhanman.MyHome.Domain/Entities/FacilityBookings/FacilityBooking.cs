using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.FacilityBookings;

public class FacilityBooking : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    #region Properties
    public int Id { get; set; }
    public Guid EventId { get; set; }
    public string Name { get; set; }
    public int BuildingId { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }
    #endregion

    #region Constructor
    public FacilityBooking() { }
    public FacilityBooking(int id, Guid eventId, string name, int buildingId)
    {
        Id = id;
        EventId = eventId;
        Name = name;
        BuildingId = buildingId;
    }
    #endregion
}

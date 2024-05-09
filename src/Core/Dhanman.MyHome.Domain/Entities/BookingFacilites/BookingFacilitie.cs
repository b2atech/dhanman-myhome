using B2aTech.CrossCuttingConcern.Core.Abstractions;
using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.BookingFacilites;

public class BookingFacilitie : EntityInt, IAuditableEntity, ISoftDeletableEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BuildingId { get; set; }
    public DateTime CreatedOnUtc { get; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; }
    public bool IsDeleted { get; }
    public Guid CreatedBy { get; protected set; }
    public Guid? ModifiedBy { get; protected set; }

    public BookingFacilitie(int id, string name, int buildingId)
    {
        Id = id;
        Name = name;
        BuildingId = buildingId;
    }
}

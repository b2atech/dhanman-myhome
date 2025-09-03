using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.Vehicals;

public class VehicleInfo : EntityInt
{
    public int Id { get; set; }
    public string VehicleNumber { get; set; }
    public int VehicleTypeId { get; set; }
    public int UnitId { get; set; }
    public string? VehicleRfId { get; set; }
    public string? VehicleRfIdSecretcode { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool IsDeleted { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? ModifiedBy { get; set; }
}

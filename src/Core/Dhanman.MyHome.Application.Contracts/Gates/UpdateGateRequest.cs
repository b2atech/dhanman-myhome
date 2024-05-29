namespace Dhanman.MyHome.Application.Contracts.Gates;

public sealed class UpdateGateRequest
{
    #region Properties
    public int GateId { get; set; }
    public string Name { get; set; }
    public Guid ApartmentId { get; set; }
    public int? BuildingId { get; set; }
    public int GateTypeId { get; set; }
    public bool IsUsedForIn { get; set; }
    public bool IsUsedForOut { get; set; }
    public bool IsAllUsersAllowed { get; set; }
    public bool IsResidentsAllowed { get; set; }
    public bool IsStaffAllowed { get; set; }
    public bool IsVendorAllowed { get; set; }
    #endregion

    public UpdateGateRequest() => Name = string.Empty;
}

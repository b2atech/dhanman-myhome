namespace Dhanman.MyHome.Application.Contracts.Gates;

public class CreateGateRequest
{
    #region Properties
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

    #region Constructor
    public CreateGateRequest()
    {
    }
    #endregion
}

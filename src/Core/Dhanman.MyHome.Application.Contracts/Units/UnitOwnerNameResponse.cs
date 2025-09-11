namespace Dhanman.MyHome.Application.Contracts.Units;

public class UnitOwnerNameResponse
{
    public int UnitOwnerNameId { get; set; }
    public Guid CustomerId { get; set; }
    public int UnitId { get; set; }
    public string UnitName { get; set; } = string.Empty;
    public string OwnerFirstName { get; set; } = string.Empty;
    public string OwnerLastName { get; set; } = string.Empty;

    public UnitOwnerNameResponse(int unitOwnerNameId, Guid customerId, int unitId, string unitName, string ownerFirstName, string ownerLastName)
    {
        UnitOwnerNameId = unitOwnerNameId;
        CustomerId = customerId;
        UnitId = unitId;
        UnitName = unitName;
        OwnerFirstName = ownerFirstName;
        OwnerLastName = ownerLastName;
    }
}

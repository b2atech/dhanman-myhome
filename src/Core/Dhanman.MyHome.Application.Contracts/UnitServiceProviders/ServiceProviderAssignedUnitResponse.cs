namespace Dhanman.MyHome.Application.Contracts.UnitServiceProviders;

public sealed class ServiceProviderAssignedUnitResponse
{
    #region Properties 
    public int Id { get; set; }
    public int UnitId  { get; set; }

    public string Name { get; set; }

    #endregion

    #region Constructor
    public ServiceProviderAssignedUnitResponse(int id, int unitId, string name)
    {
        Id = id;
        UnitId = unitId;
        Name = name;
    }
    #endregion
}

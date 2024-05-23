namespace Dhanman.MyHome.Application.Contracts.UnitServiceProviders;

public sealed class AssignSPUnitsResponse
{
    #region Properties 
    public int Id { get; set; }
    public string Name { get; set; }
    public Guid CustomerId { get; set; }

    #endregion

    #region Constructor
    public AssignSPUnitsResponse(int id, string name, Guid customerId)
    {
        Id = id;
        Name = name;
        CustomerId = customerId;
    }
    #endregion
}

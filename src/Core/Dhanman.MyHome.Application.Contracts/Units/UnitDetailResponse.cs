namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitDetailResponse
{
    #region Properties 
    public int Id { get; set; }     
    public string Name { get; set; }
    public Guid CustomerId { get; set; }
    public decimal SqftArea { get; set; }
    public decimal BHK { get; set; }

    #endregion

    #region Constructor
    public UnitDetailResponse(int id, string name, Guid customerId, decimal sqftArea, decimal bHK)
    {
        Id = id;
        Name = name;
        CustomerId = customerId;
        SqftArea = sqftArea;
        BHK = bHK;
    }
    #endregion
}
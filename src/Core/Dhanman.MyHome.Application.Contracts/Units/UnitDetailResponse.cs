namespace Dhanman.MyHome.Application.Contracts.Units;

public sealed class UnitDetailResponse
{
    #region Properties 
    public int Id { get; set; }     
    public string Name { get; set; }
    public Guid CustomerId { get; set; }
    public Guid AccountId { get; set; }
    public decimal SqftArea { get; set; }
    public decimal BHK { get; set; }

    #endregion

    #region Constructor
    public UnitDetailResponse(int id, string name, Guid customerId, Guid accountId, decimal sqftArea, decimal bHK)
    {
        Id = id;
        Name = name;
        CustomerId = customerId;
        AccountId = accountId;
        SqftArea = sqftArea;
        BHK = bHK;
    }
    #endregion
}
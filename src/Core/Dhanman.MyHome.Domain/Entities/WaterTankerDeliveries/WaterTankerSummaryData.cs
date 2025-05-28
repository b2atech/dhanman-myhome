using B2aTech.CrossCuttingConcern.Core.Primitives;

namespace Dhanman.MyHome.Domain.Entities.WaterTankerDeliveries;

public class WaterTankerSummaryData : EntityInt
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalTankers { get; set; }
    public decimal TotalLiters { get; set; }
}

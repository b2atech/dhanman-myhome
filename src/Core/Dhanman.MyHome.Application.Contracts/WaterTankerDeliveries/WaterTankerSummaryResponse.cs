namespace Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;

public sealed class WaterTankerSummaryResponse
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public int TotalTankers { get; init; }
    public decimal TotalLiters { get; init; }

    public WaterTankerSummaryResponse(DateTime start, DateTime end, int totalTankers, decimal totalLiters)
    {
        StartDate = start;
        EndDate = end;
        TotalTankers = totalTankers;
        TotalLiters = totalLiters;
    }
}
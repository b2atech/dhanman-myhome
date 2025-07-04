using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Queries;

public sealed class GetWaterTankerSummaryQuery : IQuery<Result<WaterTankerSummaryResponse>>
{
    public Guid CompanyId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    public GetWaterTankerSummaryQuery(Guid companyId, DateTime startDate, DateTime endDate)
    {
        CompanyId = companyId;
        StartDate = startDate;
        EndDate = endDate;
    }
}

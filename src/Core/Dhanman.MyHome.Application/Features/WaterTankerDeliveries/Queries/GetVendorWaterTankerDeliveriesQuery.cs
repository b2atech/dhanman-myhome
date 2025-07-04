using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.WaterTankerDeliveries;

namespace Dhanman.MyHome.Application.Features.WaterTankerDeliveries.Queries;

public sealed class GetVendorWaterTankerDeliveriesQuery : IQuery<Result<WaterTankerDeliveryListResponse>>
{
    public Guid CompanyId { get; init; }
    public Guid VendorId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    public GetVendorWaterTankerDeliveriesQuery(Guid companyId, Guid vendorId, DateTime startDate, DateTime endDate)
    {
        CompanyId = companyId;
        VendorId = vendorId;
        StartDate = startDate;
        EndDate = endDate;
    }
}
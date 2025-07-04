using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Portfolios;

namespace Dhanman.MyHome.Application.Features.Portfolios.Queries;

public sealed class GetPortfoliosByApartmentQuery : IQuery<Result<PortfolioListResponse>>
{
    public Guid ApartmentId { get; }

    public GetPortfoliosByApartmentQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }
}
using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using MediatR;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetAllUnitNamesByApartmentIdQuery : IQuery<Result<UnitNamesWithApartmentListResponse>>
{
    public Guid ApartmentId { get; }

    public GetAllUnitNamesByApartmentIdQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }
}

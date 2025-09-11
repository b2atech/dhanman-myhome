using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Units.Queries;
public class GetUnitsWithPrimaryOwnerQuery : IQuery<Result<UnitOwnerNameListResponse>>
{
    public Guid ApartmentId { get; }

    public GetUnitsWithPrimaryOwnerQuery(Guid apartmentId)
    {
        ApartmentId = apartmentId;
    }
}

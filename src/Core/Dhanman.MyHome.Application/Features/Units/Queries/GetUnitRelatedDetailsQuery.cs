using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.Shared.Contracts.Abstractions.Messaging;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetUnitRelatedDetailsQuery : ICacheableQuery<Result<UnitRelatedDetailsDto>>
{
    public int UnitId { get; }

    public GetUnitRelatedDetailsQuery(int unitId)
    {
        UnitId = unitId;
    }

    public string GetCacheKey() => $"unit_related_details_{UnitId}";
}

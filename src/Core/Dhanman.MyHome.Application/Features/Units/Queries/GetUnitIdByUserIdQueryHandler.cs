using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Residents;
using Dhanman.MyHome.Domain.Entities.ResidentUnits;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetUnitIdByUserIdQueryHandler : IQueryHandler<GetUnitIdByUserIdQuery, Result<GetUnitIdbyUserIdResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public GetUnitIdByUserIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<GetUnitIdbyUserIdResponse>> Handle(GetUnitIdByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {
                var unitIds = await (from ru in _dbContext.SetInt<ResidentUnit>().AsNoTracking()
                                    join r in _dbContext.SetInt<Resident>().AsNoTracking()
                                on ru.ResidentId equals r.Id
                                    where r.UserId == request.UserId
                                       && r.ApartmentId == request.ApartmentId
                                    select ru.UnitId)
                                    .ToListAsync(cancellationToken);
                if (unitIds == null || !unitIds.Any())
                {
                    return Result.Failure<GetUnitIdbyUserIdResponse>(Errors.General.EntityNotFound);
                }

                var response = new GetUnitIdbyUserIdResponse(request.UserId, request.ApartmentId, unitIds);

                return Result.Success(response);
            });
    }
    #endregion

}
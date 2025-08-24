using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Units;
using Dhanman.MyHome.Domain;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Units;

namespace Dhanman.MyHome.Application.Features.Units.Queries;

public class GetUnitIdByUserIdQueryHandler : IQueryHandler<GetUnitIdByUserIdQuery, Result<BasicUnitInfoResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructor
    public GetUnitIdByUserIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<BasicUnitInfoResponse>> Handle(GetUnitIdByUserIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
            .Ensure(query => query != null, Errors.General.EntityNotFound)
            .Bind(async query =>
            {

                var unit = await _dbContext.SetInt<UnitInfo>()
                                   .FromSqlInterpolated(
                                       $"SELECT * FROM public.get_unit_info_by_user_apartment({request.UserId}, {request.ApartmentId})"
                                   )
                                   .AsNoTracking()
                                   .Select(u => new BasicUnitInfo(u.Id, u.UnitId, u.UnitName, u.UserName, u.ApartmentId))
                                   .FirstOrDefaultAsync(cancellationToken);

                if (unit is null)
                    return Result.Failure<BasicUnitInfoResponse>(Errors.General.EntityNotFound);

                var response = new BasicUnitInfoResponse(unit);
                return Result.Success(response);
            });
    }


    #endregion

}
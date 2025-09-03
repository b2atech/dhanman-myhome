using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using NpgsqlTypes;

namespace Dhanman.MyHome.Application.Features.Residents.Queries;

public class GetAllResidentNamesQueryHandler : IQueryHandler<GetAllResidentNamesQuery, Result<ResidentNameListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllResidentNamesQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ResidentNameListResponse>> Handle(GetAllResidentNamesQuery request, CancellationToken cancellationToken)
    {
        var apartmentId = new NpgsqlParameter("apartmentid", NpgsqlDbType.Uuid) { Value = request.ApartmentId };
        var isGetAll = new NpgsqlParameter("isGetAll", NpgsqlDbType.Boolean) { Value = request.IsGetAll };

        var residentNames = await _dbContext.SetInt<ResidentNames>()
            .FromSqlRaw("SELECT * FROM get_all_apartment_or_org_residents(@apartmentid, @isGetAll)", apartmentId, isGetAll)
            .Select(v => new ResidentNameResponse(
                v.Id,
                v.ResidentName,
                v.UserId
            ))
            .ToListAsync(cancellationToken);

        var listResponse = new ResidentNameListResponse(residentNames);
        return Result.Success(listResponse);
    }

    #endregion

}

﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Buildings;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.BuildingTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Buildings.Queries;

public class GetBuildingByIdQueryHandler : IQueryHandler<GetBuildingByIdQuery, Result<BuildingResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetBuildingByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<BuildingResponse>> Handle(GetBuildingByIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var buildings = await _dbContext.SetInt<Building>()
                  .AsNoTracking()
                  .Where(e => e.Id == query.BuildingId)
                  .OrderBy(e => e.Id)
                  .Select(e => new BuildingResponse(
                          e.Id,
                          e.Name,
                          e.BuildingTypeId,
                          _dbContext.SetInt<BuildingType>()
                              .Where(p => p.Id == e.BuildingTypeId)
                              .Select(p => p.Name).FirstOrDefault(),
                          e.TotalUnits,
                          e.CreatedOnUtc,
                          e.ModifiedOnUtc,
                          e.CreatedBy,
                          e.ModifiedBy))
                  .FirstOrDefaultAsync(cancellationToken);

                  return buildings;
              });
    }
    #endregion

}
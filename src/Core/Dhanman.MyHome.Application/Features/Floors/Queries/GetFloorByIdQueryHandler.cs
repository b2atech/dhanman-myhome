﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Floors;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.Floors;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Floors.Queries;

public class GetFloorByIdQueryHandler : IQueryHandler<GetFloorByIdQuery, Result<FloorResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetFloorByIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<FloorResponse>> Handle(GetFloorByIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {

                  var floorResponse = await (from floor in _dbContext.SetInt<Floor>().AsNoTracking()
                                     where floor.Id == request.FloorId
                                     join building in _dbContext.SetInt<Building>().AsNoTracking()
                                     on floor.BuildingId equals building.Id
                                     select new FloorResponse(
                                         floor.Id,
                                         floor.Name,
                                         floor.ApartmentId,
                                         floor.BuildingId,
                                         building.Name,
                                         floor.TotalUnits,
                                         floor.CreatedBy,
                                         floor.CreatedOnUtc,
                                         floor.ModifiedBy,
                                         floor.ModifiedOnUtc))
                                        .FirstOrDefaultAsync(cancellationToken);

                  return floorResponse != null
                ? Result.Success(floorResponse)
                : Result.Failure<FloorResponse>(Errors.General.EntityNotFound);

              });
    }
    #endregion
}

﻿using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Floors;
using Microsoft.EntityFrameworkCore;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.Floors;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Users;

namespace Dhanman.MyHome.Application.Features.Floors.Queries;

public class GetAllFloorsQueryHandler : IQueryHandler<GetAllFloorsQuery, Result<FloorListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllFloorsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<FloorListResponse>> Handle(GetAllFloorsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var floors = await (from floor in _dbContext.SetInt<Floor>().AsNoTracking()
                                      where floor.ApartmentId == request.ApartmentId
                                      join building in _dbContext.SetInt<Building>().AsNoTracking()
                                      on floor.BuildingId equals building.Id
                                      join createdByUser in _dbContext.Set<User>()
                                         on floor.CreatedBy
                                         equals createdByUser.Id into createdByUserGroup
                                      from createdByUser in createdByUserGroup.DefaultIfEmpty() // Left join for CreatedBy user
                                      join modifiedByUser in _dbContext.Set<User>()
                                          on floor.ModifiedBy
                                          equals modifiedByUser.Id into modifiedByUserGroup
                                      from modifiedByUser in modifiedByUserGroup.DefaultIfEmpty() // Left join for ModifiedBy user
                                      select new FloorResponse(
                                          floor.Id,
                                          floor.Name,
                                          floor.ApartmentId,
                                          floor.BuildingId,
                                          building.Name,
                                          floor.TotalUnits,
                                          floor.CreatedBy,
                                          floor.ModifiedBy,
                                          floor.CreatedOnUtc,
                                          floor.ModifiedOnUtc,
                                          $"{createdByUser.FirstName.Value} {createdByUser.LastName.Value}",
                                          $"{modifiedByUser.FirstName.Value} {modifiedByUser.LastName.Value}"
                                          ))
                      .ToListAsync(cancellationToken);

                  var listResponse = new FloorListResponse(floors);

                  return listResponse;
              });
    }
    #endregion

}
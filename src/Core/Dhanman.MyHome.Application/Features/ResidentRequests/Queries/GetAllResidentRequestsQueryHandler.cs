using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.ResidentRequests;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Dhanman.MyHome.Domain.Entities.Buildings;
using Dhanman.MyHome.Domain.Entities.Floors;
using Dhanman.MyHome.Domain.Entities.ResidentRequests;
using Dhanman.MyHome.Domain.Entities.ResidentStatuses;
using Dhanman.MyHome.Domain.Entities.ResidentTypes;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.UnitStatuses;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.ResidentRequests.Queries;

public class GetAllResidentRequestsQueryHandler : IQueryHandler<GetAllResidentRequestsQuery, Result<ResidentRequestListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllResidentRequestsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<ResidentRequestListResponse>> Handle(GetAllResidentRequestsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var residentRequests = await _dbContext.SetInt<ResidentRequest>()
                  .AsNoTracking()
                   .Select(e => new
                   {
                       e.Id,
                       e.UnitId,
                       e.FirstName,
                       e.LastName,
                       e.Email,
                       e.ContactNumber,
                       e.PermanentAddressId,
                       e.RequestStatusId,
                       e.ResidentTypeId,
                       e.OccupancyStatusId,
                       e.CreatedOnUtc,
                       e.CreatedBy,
                       e.ModifiedOnUtc,
                       e.ModifiedBy,
                       Unit = _dbContext.SetInt<Unit>()
                            .Where(u => u.Id == e.UnitId)
                            .Select(u => new
                            {
                                u.FloorId,
                                u.BuildingId,
                                u.ApartmentId,
                                UnitName = u.Name
                            }).FirstOrDefault()
                   })
                  .Select(e => new ResidentRequestResponse(
                      e.Id,
                      e.Unit.ApartmentId,
                      _dbContext.Set<Apartment>()
                              .Where(p => p.Id == e.Unit.ApartmentId)
                              .Select(p => p.Name).FirstOrDefault(),
                      e.Unit.BuildingId,
                      _dbContext.SetInt<Building>()
                              .Where(p => p.Id == e.Unit.BuildingId)
                              .Select(p => p.Name).FirstOrDefault(),
                      e.Unit.FloorId,
                      _dbContext.SetInt<Floor>()
                              .Where(p => p.Id == e.Unit.FloorId)
                              .Select(p => p.Name).FirstOrDefault(),
                      e.UnitId,
                      e.Unit.UnitName,
                          e.FirstName,
                          e.LastName,
                          e.Email,
                          e.ContactNumber,
                          e.PermanentAddressId,
                          e.RequestStatusId,
                          _dbContext.SetInt<ResidentRequestStatus>()
                                  .Where(p => p.Id == e.RequestStatusId)
                                  .Select(p => p.Name).FirstOrDefault(),
                          e.ResidentTypeId,
                          _dbContext.SetInt<ResidentType>()
                                  .Where(p => p.Id == e.ResidentTypeId)
                                  .Select(p => p.Name).FirstOrDefault(),
                          e.OccupancyStatusId,
                               _dbContext.SetInt<UnitStatus>()
                                  .Where(p => p.Id == e.OccupancyStatusId)
                                  .Select(p => p.Name).FirstOrDefault(),
                          e.CreatedOnUtc,
                          e.CreatedBy,
                          e.ModifiedOnUtc,
                          e.ModifiedBy))
                  .ToListAsync(cancellationToken);

                  var listResponse = new ResidentRequestListResponse(residentRequests);

                  return listResponse;
              });
    }
    //public async Task<Result<ResidentRequestListResponse>> Handle(GetAllResidentRequestsQuery request, CancellationToken cancellationToken)
    //{
    //    return await Result.Success(request)
    //          .Ensure(query => query != null, Errors.General.EntityNotFound)
    //          .Bind(async query =>
    //          {
    //              var residentRequests = await _dbContext.SetInt<ResidentRequest>()
    //              .AsNoTracking()
    //              .Select(e => new ResidentRequestResponse(
    //                      e.Id,
    //                      e.ApartmentId,
    //                      _dbContext.Set<Apartment>()
    //                              .Where(p => p.Id == e.ApartmentId)
    //                              .Select(p => p.Name).FirstOrDefault(),
    //                      e.BuildingId,
    //                      _dbContext.SetInt<Building>()
    //                              .Where(p => p.Id == e.BuildingId)
    //                              .Select(p => p.Name).FirstOrDefault(),
    //                      e.FloorId,
    //                      _dbContext.SetInt<Floor>()
    //                              .Where(p => p.Id == e.FloorId)
    //                              .Select(p => p.Name).FirstOrDefault(),
    //                      e.UnitId,
    //                      _dbContext.SetInt<Unit>()
    //                              .Where(p => p.Id == e.UnitId)
    //                              .Select(p => p.Name).FirstOrDefault(),
    //                      e.FirstName,
    //                      e.LastName,
    //                      e.Email,
    //                      e.ContactNumber,
    //                      e.PermanentAddressId,
    //                      e.RequestStatusId,
    //                      _dbContext.SetInt<ResidentRequestStatus>()
    //                              .Where(p => p.Id == e.RequestStatusId)
    //                              .Select(p => p.Name).FirstOrDefault(),
    //                      e.ResidentTypeId,
    //                      _dbContext.SetInt<ResidentType>()
    //                              .Where(p => p.Id == e.ResidentTypeId)
    //                              .Select(p => p.Name).FirstOrDefault(),
    //                      e.OccupancyStatusId,
    //                           _dbContext.SetInt<UnitStatus>()
    //                              .Where(p => p.Id == e.OccupancyStatusId)
    //                              .Select(p => p.Name).FirstOrDefault(),
    //                      e.CreatedOnUtc,
    //                      e.CreatedBy,
    //                      e.ModifiedOnUtc,                          
    //                      e.ModifiedBy))
    //              .ToListAsync(cancellationToken);

    //              var listResponse = new ResidentRequestListResponse(residentRequests);

    //              return listResponse;
    //          });
    //}
    #endregion

}
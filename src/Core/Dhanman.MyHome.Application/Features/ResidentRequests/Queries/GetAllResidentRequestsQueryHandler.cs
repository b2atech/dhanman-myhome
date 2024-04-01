using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.ResidentRequests;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
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
                  .Select(e => new ResidentRequestResponse(
                          e.Id,
                          e.ApartmentId,
                          _dbContext.Set<Apartment>()
                                  .Where(p => p.Id == e.ApartmentId)
                                  .Select(p => p.Name).FirstOrDefault(),
                          e.BuildingId,
                          _dbContext.SetInt<Building>()
                                  .Where(p => p.Id == e.BuildingId)
                                  .Select(p => p.Name).FirstOrDefault(),
                          e.FloorId,
                          _dbContext.SetInt<Floor>()
                                  .Where(p => p.Id == e.FloorId)
                                  .Select(p => p.Name).FirstOrDefault(),
                          e.UnitId,
                          _dbContext.SetInt<Unit>()
                                  .Where(p => p.Id == e.UnitId)
                                  .Select(p => p.Name).FirstOrDefault(),
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
    #endregion

}
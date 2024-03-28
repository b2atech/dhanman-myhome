using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Residents;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Residents.Queries;
public class GetAllResidentsQueryHandler : IQueryHandler<GetAllResidentsQuery, Result<ResidentListResponse>>
{
#region Properties
private readonly IApplicationDbContext _dbContext;
#endregion

#region Constructors
public GetAllResidentsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
#endregion

#region Methods
public async Task<Result<ResidentListResponse>> Handle(GetAllResidentsQuery request, CancellationToken cancellationToken)
{
    return await Result.Success(request)
          .Ensure(query => query != null, Errors.General.EntityNotFound)
          .Bind(async query =>
          {
              var residents = await _dbContext.SetInt<Resident>()
              .AsNoTracking()                  
              .Select(e => new ResidentResponse(
                      e.Id,   
                      e.UnitId,
                      _dbContext.SetInt<Unit>()
                              .Where(p => p.Id == e.UnitId)
                              .Select(p => p.Name).FirstOrDefault(),
                      e.FirstName,
                      e.LastName,
                      e.Email,
                      e.ContactNumber,
                      e.ResidentTypeId,
                      _dbContext.SetInt<ResidentType>()
                              .Where(p => p.Id == e.ResidentTypeId)
                              .Select(p => p.Name).FirstOrDefault(),
                      e.OccupancyStatusId,
                           _dbContext.SetInt<UnitStatus>()
                              .Where(p => p.Id == e.Id)
                              .Select(p => p.Name).FirstOrDefault(),
                      e.CreatedOnUtc,
                      e.ModifiedOnUtc,
                      e.CreatedBy,
                      e.ModifiedBy))
              .ToListAsync(cancellationToken);

              var listResponse = new ResidentListResponse(residents);

              return listResponse;
          });
}
#endregion

}
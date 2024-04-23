using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.MyHome.Application.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Vehicles;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.Apartments;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries;
public class GetAllVisitorsQueryHandler : IQueryHandler<GetAllVisitorsQuery, Result<VisitorListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetAllVisitorsQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<VisitorListResponse>> Handle(GetAllVisitorsQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  var visitors = await _dbContext.SetInt<Visitor>()
                  .AsNoTracking()
                  .Select(e => new VisitorResponse(
                          e.Id,
                          e.FirstName,
                          e.LastName,
                          e.Email,
                          e.VisitingFrom,
                          e.ContactNumber,
                          e.VisitorTypeId,
                          _dbContext.SetInt<VisitorType>()
                                  .Where(p => p.Id == e.VisitorTypeId)
                                  .Select(p => p.Name).FirstOrDefault(),
                          e.VehicleNumber,
                          e.IdentityTypeId,
                          e.IdentityNumber,
                          e.CreatedBy,
                          e.CreatedOnUtc,
                          e.ModifiedBy,
                          e.ModifiedOnUtc))
                  .ToListAsync(cancellationToken);

                  var listResponse = new VisitorListResponse(visitors);

                  return listResponse;
              });
    }
    #endregion

}
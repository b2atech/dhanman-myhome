using B2aTech.CrossCuttingConcern.Core.Result;
using Dhanman.MyHome.Application.Abstractions.Data;
using Dhanman.Shared.Contracts.Abstractions.Messaging;
using Dhanman.MyHome.Application.Contracts.Visitors;
using Dhanman.MyHome.Domain;
using Dhanman.MyHome.Domain.Entities.ServiceProviders;
using Dhanman.MyHome.Domain.Entities.Units;
using Dhanman.MyHome.Domain.Entities.Visitors;
using Dhanman.MyHome.Domain.Entities.VisitorTypes;
using Microsoft.EntityFrameworkCore;

namespace Dhanman.MyHome.Application.Features.Visitors.Queries;

public class GetVisitorsByUnitIdQueryHandler : IQueryHandler<GetVisitorsByUnitIdQuery, Result<VisitorsByUnitIdListResponse>>
{
    #region Properties
    private readonly IApplicationDbContext _dbContext;
    #endregion

    #region Constructors
    public GetVisitorsByUnitIdQueryHandler(IApplicationDbContext dbContext) => _dbContext = dbContext;
    #endregion

    #region Methods
    public async Task<Result<VisitorsByUnitIdListResponse>> Handle(GetVisitorsByUnitIdQuery request, CancellationToken cancellationToken)
    {
        return await Result.Success(request)
              .Ensure(query => query != null, Errors.General.EntityNotFound)
              .Bind(async query =>
              {
                  Guid apartmentId = _dbContext.SetInt<Unit>()
                           .Where(e => e.Id == request.UnitId)
                           .Select(e => e.ApartmentId)
                           .FirstOrDefault();

                  var visitorsbyUnitId = await (from u in _dbContext.SetInt<Unit>().AsNoTracking()
                                                join v in _dbContext.SetInt<Visitor>() on u.ApartmentId equals v.ApartmentId
                                                join sp in _dbContext.SetInt<ServiceProvider>() on v.ApartmentId equals sp.ApartmentId
                                                join vt in _dbContext.SetInt<VisitorType>() on sp.Id equals vt.Id
                                                join spt in _dbContext.SetInt<Domain.Entities.ServiceProviderTypes.ServiceProviderType>() on sp.Id equals spt.Id into sptJoin
                                                from spt in sptJoin.DefaultIfEmpty()
                                                join sps in _dbContext.SetInt<Domain.Entities.ServiceProviderSubTypes.ServiveProviderSubType>() on spt.Id equals sps.ServiceProviderTypeId into spsJoin
                                                from sps in spsJoin.DefaultIfEmpty()
                                                where u.Id == query.UnitId && u.ApartmentId == apartmentId && (vt.Id == 1 || vt.Id == 3)
                                                select new VisitorsByUnitIdResponse(
                                                    v.Id,
                                                    v.FirstName + " " + v.LastName,
                                                    u.Id,
                                                    u.Name,
                                                    vt.Id,
                                                    vt.Name,
                                                    sps.Id,
                                                    sps.Name
                                                )).ToListAsync(cancellationToken);

                  var listResponse = new VisitorsByUnitIdListResponse(visitorsbyUnitId);

                  return listResponse;                  
              });
    }
    #endregion

}